using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Crud;
using DataAccess.Entity;
using DataAccess.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using DataAccess.Dto;
using Famtastic.Util;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Text.RegularExpressions;
using Biz;

namespace Famtastic.Controllers
{
    public class FamiliesController : Controller
    {
	    private readonly FamDbContext _context;
		private readonly SignInManager<AspNetUser> _signInManager;
	    private readonly IDbReadService _dbRead;
	    private readonly IDbWriteService _dbWrite;
		
        public FamiliesController(FamDbContext context, SignInManager<AspNetUser> signInManager, IDbReadService dbRead, IDbWriteService dbWrite)
        {
	        _context = context;
	        _signInManager = signInManager;
	        _dbRead = dbRead;
	        _dbWrite = dbWrite;
        }

		public async Task<IActionResult> Details(int? id)
        {
	        if (id == null)
            {
                return NotFound();
            }

            var family = await _dbRead.GetSingleRecordAsync<Family>(f => f.Id.Equals(id));

            if (family == null)
            {
                return NotFound();
            }

            UserData.FamilyMembers = await _dbRead.GetAllRecordsAsync<UserProfile>(m => !m.FamilyId.Equals(null) && m.FamilyId.Equals(UserData.FamilyId));

            UserData.FamName = family.FamilyLastName;

            return View(family);
        }

		[HttpPost, ActionName("GetFamilyMemberProfile")]
		public async Task<UserProfile> GetFamilyMemberProfile(int userProfileId)
		{
			var user = await UserHelper.GetUserProfileAsync(userProfileId);
			TempData["UserFullName"] = user.FirstName + " " + user.LastName;
			TempData["UserBlurb"] = user.Blurb;
			TempData["Email"] = user.Email;
			TempData["UserId"] = user.Id;
			return user;
		}

		public IActionResult Create()
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FamilyLastName,Title,Picture,PictureContentType,Description,InvitationCode,FamilyAdminUserProfileId,CreatedOn,CreatedBy")] Family family, IFormFile file, string emailInvite)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

	        if (!string.IsNullOrEmpty(emailInvite))
	        {
		        EmailHelper.SendSubscriberMessage(emailInvite, Constants.EmailInvitationCode);
	        }

			// MediaHelper does the work, just send the file over.
			family.Picture = MediaHelper.ResizeImageFromFile(file);
	        
			// byte array was returned with 0 length meaning the file is too large, try again!
	        if (family.Picture.Length == 0)
	        {
		        TempData["notifyUserWarning"] = Constants.ImageMediaExceedsSizeLimit;
		        return RedirectToAction("Create", "Families");
			}

			family.PictureContentType = file.ContentType;
			family.CreatedBy = User.Identity.Name;
			family.CreatedOn = DateTime.Now;
			family.FamilyAdminUserProfileId = UserData.UserProfileId;
			
			if (ModelState.IsValid)
			{
				_dbWrite.Add<Family>(family);
				await _dbWrite.SaveChangesAsync();
			}
			
			var user = _dbRead.GetSingleRecordAsync<UserProfile>(u => u.Id.Equals(UserData.UserProfileId)).Result;
			user.FamilyId = family.Id;
			await _dbWrite.SaveChangesAsync();

			UserData.FamilyId = family.Id;
			UserData.FamilyProfileComplete = true;

			return RedirectToAction("Details", "Families", new {id = user.FamilyId});
        }

		[HttpPost]
        public async Task<IActionResult> JoinFamily(string invitationCode, int Id)
        {
	        var family = await _dbRead.GetSingleRecordAsync<Family>(u => u.Id.Equals(Id));
	        if (family != null)
	        {
		        if (family.InvitationCode == invitationCode)
		        {
			        var user = await _dbRead.GetSingleRecordAsync<UserProfile>(u =>
				        u.Id.Equals(UserData.UserProfileId));
			        if (user.FamilyId == null)
			        {
				        user.FamilyId = Id;
						_dbWrite.Update(user);
						await _dbWrite.SaveChangesAsync();
			        }
		        }
		        else
		        {
					TempData["notifyUserWarning"] = "There was a problem with your invitation code or family ID";
				}
			}

	        return RedirectToAction("Index", "Home");
		}

        public async Task<IActionResult> Edit(int? id)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

			if (id == null)
            {
                return NotFound();
            }

            var family = await _dbRead.GetSingleRecordAsync<Family>(f => f.Id.Equals(id));

            UserData.FamilyImage = family.Picture;

            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FamilyLastName,Title,Picture,PictureContentType,Description,InvitationCode,FamilyAdminUserProfileId,CreatedOn,CreatedBy")] Family family, IFormFile file, string emailInvite)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

	        if (id != family.Id)
	        {
		        return NotFound();
	        }

			if (file != null)
			{
				// MediaHelper does the work, just send the file over.
				family.Picture = MediaHelper.ResizeImageFromFile(file);

				// byte array was returned with 0 length meaning the file is too large, try again!
				if (family.Picture.Length == 0)
				{
					TempData["notifyUserWarning"] = Constants.ImageMediaExceedsSizeLimit;
					return RedirectToAction("Edit", "Families");
				}

				family.PictureContentType = file.ContentType;
			}
			else
			{
				family.Picture = UserData.FamilyImage;
			}

			if (family.Picture != null)
            {
                try
                {
                    _dbWrite.Update(family);
                    await _dbWrite.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (!string.IsNullOrEmpty(emailInvite))
                {
	                EmailHelper.SendSubscriberMessage(emailInvite, Constants.EmailInvitationCode + 
		                " Invitation Code: " + family.InvitationCode + " Family ID: " + family.Id);
                }
				return RedirectToAction("Details", "Families", new { id = family.Id });
			}
            return View(family);
        }

        public async Task<IActionResult> Delete(int? id)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

			if (id == null)
            {
                return NotFound();
            }

            var family = await _dbRead.GetSingleRecordAsync<Family>(f => f.Id.Equals(id));
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

			var family = await _dbRead.GetSingleRecordAsync<Family>(f => f.Id.Equals(id));
			_dbWrite.Delete(family);
			await _dbWrite.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.Id == id);
        }
    }
}
