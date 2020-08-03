using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class NoteListItem
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string NoteListItem1 { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual Note Note { get; set; }
    }
}
