using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Comments { get; set; }
        public bool? ShareToFam { get; set; }
        public bool? CommunityShared { get; set; }
        public int? FamilyId { get; set; }
        public int? UserProfileId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
