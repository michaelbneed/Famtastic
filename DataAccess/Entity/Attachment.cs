using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public int? NoteId { get; set; }
        public int? FamilyMessageId { get; set; }
        public int? ChatMessageId { get; set; }
        public int? RecipeId { get; set; }
    }
}
