using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int? ChatThreadId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
