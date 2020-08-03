using System;
using System.Collections.Generic;

namespace DataAccess.Entity
{
    public partial class Chat
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string OriginatingUserId { get; set; }
        public string ReceivingUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
