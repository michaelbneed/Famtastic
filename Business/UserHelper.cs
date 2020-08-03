using System;
using DataAccess.Identity;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Entity;

namespace Business
{
	public class UserHelper
	{
		private FamDbContext _context;

		public UserHelper(FamDbContext context)
		{
			_context = context;
		}

		private void CreateUserProfile(AspNetUser user)
		{
			var userProfile = new UserProfile();
			var userId = user.Id;
			var userEmail = user.Email;

			_context.UserProfile.Add(userProfile);
			_context.
		}
	}
}
