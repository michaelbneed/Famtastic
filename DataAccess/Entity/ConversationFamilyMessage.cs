using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class ConversationFamilyMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public int? ConversationId { get; set; }
        public int? UserProfileId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual ConversationFamily Conversation { get; set; }
    }
}
