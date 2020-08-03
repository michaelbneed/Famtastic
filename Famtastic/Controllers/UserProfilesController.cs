using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Biz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Crud;
using DataAccess.Dto;
using DataAccess.Entity;
using DataAccess.Identity;
using Famtastic.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Famtastic.Controllers
{
    public class UserProfilesController : Controller
    {
		private readonly FamDbContext _context;
		private readonly SignInManager<AspNetUser> _signInManager;
		private readonly IDbReadService _dbRead;
		private readonly IDbWriteService _dbWrite;

		public UserProfilesController(FamDbContext context, SignInManager<AspNetUser> signInManager, IDbReadService dbRead, IDbWriteService dbWrite)
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

			_dbRead.IncludeEntityNavigation<Family>();
			var userProfile = await _dbRead.GetSingleRecordAsync<UserProfile>(u => u.Id.Equals(id));
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var userProfile = await _dbRead.GetSingleRecordAsync<UserProfile>(u => u.Id.Equals(id));
			if (userProfile == null)
            {
                return NotFound();
            }

			UserData.ProfileImage = userProfile.Picture;

			ViewData["FamilyId"] = new SelectList(_context.Family, "Id", "Id", userProfile.FamilyId);
            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FamilyId,Email,Profile,FirstName,LastName,Picture,PictureContentType,Blurb,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy")] UserProfile userProfile, IFormFile file)
        {
            if (id != userProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
	                if (file != null)
	                {
						// MediaHelper does the work, just send the file over.
						userProfile.Picture = MediaHelper.ResizeImageFromFile(file);

						// byte array was returned with 0 length meaning the file is too large, try again!
						if (userProfile.Picture.Length == 0)
						{
							TempData["notifyUserWarning"] = Constants.ImageMediaExceedsSizeLimit;
							return RedirectToAction("Edit", "UserProfiles");
						}

						userProfile.PictureContentType = file.ContentType;
					}
	                else
	                {
		                userProfile.Picture = UserData.ProfileImage;
	                }

					_dbWrite.Update(userProfile);
                    await _dbWrite.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (userProfile.LastName != null || userProfile.FirstName != null || userProfile.Email != null)
                {
	                UserData.UserProfileComplete = true;
                }
                else
                {
	                TempData["notifyUserWarning"] = "Your profile is not complete!";
                }

				return RedirectToAction("Details", "UserProfiles", new { Id = id});
            }
            ViewData["FamilyId"] = new SelectList(_context.Family, "Id", "Id", userProfile.FamilyId);
            return View(userProfile);
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.Id == id);
        }
    }
}