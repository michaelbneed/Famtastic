using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class ConversationFamily
    {
        public ConversationFamily()
        {
            ConversationFamilyMessage = new HashSet<ConversationFamilyMessage>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public int? FamilyId { get; set; }
        public int? UserProfileId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual Family Family { get; set; }
        public virtual ICollection<ConversationFamilyMessage> ConversationFamilyMessage { get; set; }
    }
}
