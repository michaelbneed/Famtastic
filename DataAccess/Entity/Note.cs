using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.Dto;

namespace DataAccess.Entity
{
    public partial class Note
    {
		public Note()
		{
			NoteListItem = new HashSet<NoteListItem>();
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public bool Advanced { get; set; }
		public DateTime? DueDate { get; set; }
		public int? UserProfileId { get; set; }
		public bool ShareToFam { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public virtual UserProfile UserProfile { get; set; }
		public virtual ICollection<NoteListItem> NoteListItem { get; set; }
	}
}
