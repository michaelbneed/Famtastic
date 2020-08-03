using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Media = new HashSet<Media>();
            Note = new HashSet<Note>();
            Recipe = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int? FamilyId { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
		public string PictureContentType { get; set; }
        public string Blurb { get; set; }
		public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

		public virtual Family Family { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Note> Note { get; set; }
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
