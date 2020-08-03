using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class FamilyPhotoGallery
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? FamilyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
