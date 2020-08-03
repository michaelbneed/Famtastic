using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Biz;
using DataAccess.Crud;
using DataAccess.Dto;
using DataAccess.Entity;
using DataAccess.Identity;
using Microsoft.AspNetCore.Mvc;
using Famtastic.Models;
using Famtastic.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Famtastic.Controllers
{
	public class HomeController : Controller
	{
		private readonly SignInManager<AspNetUser> _signInManager;
		private readonly IDbReadService _dbRead;
		private readonly IDbWriteService _dbWrite;
		private readonly IMediaHelper _mediaHelper;
		
		public HomeController(SignInManager<AspNetUser> signInManager, IDbReadService dbRead, IDbWriteService dbWrite,
								IMediaHelper mediaHelper)
		{
			_signInManager = signInManager;
			_dbRead = dbRead;
			_dbWrite = dbWrite;
			_mediaHelper = mediaHelper;
		}

		public async Task<IActionResult> Index()
		{
			SessionData.PristineSession();
			
			if (!_signInManager.IsSignedIn(User))
			{
				return RedirectToPage("/Account/Login", new {Area = "Identity"});
			}

			// UserData static class for state
			// Session data persisted on app entry
			UserData.UserId = _signInManager.UserManager.GetUserId(User);
			var userProfile = await _dbRead.GetSingleRecordAsync<UserProfile>(u => u.UserId.Equals(UserData.UserId));
			UserData.UserProfileId = userProfile.Id;

			Family family = new Family();
			if (userProfile.FamilyId != null)
			{
				family = await _dbRead.GetSingleRecordAsync<Family>(f => f.Id.Equals(userProfile.FamilyId));
				UserData.FamilyId = family.Id;
				UserData.FamilyProfileComplete = true;
			}
			else
			{
				UserData.FamilyProfileComplete = false;
			}

			if (userProfile.LastName == null || userProfile.FirstName == null || userProfile.Email == null)
			{
				UserData.UserProfileComplete = false;
			}
			else
			{
				UserData.UserProfileComplete = true;
			}
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
