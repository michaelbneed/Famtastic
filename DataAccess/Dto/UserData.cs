using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entity;

namespace DataAccess.Dto
{
	public static class UserData
	{
		public static string UserId { get; set; }

		public static int UserProfileId { get; set; }

		public static int FamilyId { get; set; }

		public static string FamName { get; set; }

		public static byte[] FamilyImage { get; set; }

		public static byte[] ProfileImage { get; set; }

		public static bool FamilyProfileComplete { get; set; }

		public static bool UserProfileComplete { get; set; }

		public static List<UserProfile> FamilyMembers { get; set; }
	}
}
