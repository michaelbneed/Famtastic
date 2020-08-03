using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Entity
{
    public partial class Family
    {
        public Family()
        {
            ConversationFamily = new HashSet<ConversationFamily>();
            UserProfile = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string FamilyLastName { get; set; }
        public string Title { get; set; }
        public byte[] Picture { get; set; }
        public string PictureContentType { get; set; }
		public string Description { get; set; }
		public string InvitationCode { get; set; }
		public int FamilyAdminUserProfileId { get; set; }
		public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<ConversationFamily> ConversationFamily { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
