using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Famtastic.Util
{
	public class SessionData
	{
		public static void PristineSession()
		{
			UserData.FamilyId = 0;
			UserData.UserProfileId = 0;
			UserData.UserId = null;
			UserData.UserProfileComplete = false;
			UserData.FamilyProfileComplete = false;
			UserData.FamName = null;
			UserData.FamilyImage = null;
			UserData.ProfileImage = null;
			UserData.FamilyMembers = null;
		}
	}
}
