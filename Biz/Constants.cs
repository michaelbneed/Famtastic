using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Biz
{
	public static class Constants
	{
		public const string RenderFamilyImage = "FamilyImage";
		public const string RenderProfileImage = "ProfileImage";
		public const string RenderMediaImage = "MediaImage";
		public const string RenderMediaVideo = "MediaVideo";

		public const string NotifyUserProfileIncomplete = "Your user profile is not complete. Click the Profile tab to finish setting up your screen account. ";

		public const string NotifyFamilyProfileIncomplete = "This product has many great features for individuals that a single app consumer to use. Features like notes, " +
		                                                    "lists, recipes, liked places and basic wallet-in-a-phone. We notice you are not " +
		                                                    "linked to a Famtastic family yet. Click on the family panel to create or join a family. ";

		public const string ImageMediaExceedsSizeLimit = "The image media being uploaded is larger than pernmitted. Please limit images to 5MB. ";

		public const string VideoMediaExceedsSizeLimit = "The video media being uploaded is larger than pernmitted. Please limit images to 100MB. ";

		public const string EmailInvitationCode = "Famtastic Invitation! Come join your family... If you are not registered then please register prior to " +
		                                          "joining a family, then use the invitation code you have received. Visit https://www.famtastic.us.";
	}
}
