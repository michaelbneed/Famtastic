using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
		public byte[] Video { get; set; }
		public string VideoContentType { get; set; }
		public int? UserProfileId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
