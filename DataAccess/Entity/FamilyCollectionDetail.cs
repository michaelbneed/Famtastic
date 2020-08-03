using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class FamilyCollectionDetail
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public bool? Advanced { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public int? FamilyCollectionId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
