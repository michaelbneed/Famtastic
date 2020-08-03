using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class FamilyCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? CommunityShared { get; set; }
        public int? FamilyId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
