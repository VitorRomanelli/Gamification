using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class TrelloActionLog
    {
        public Guid Id { get; set; }
        public string TrelloPayload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid? AffectedOrderId { get; set; }
        public Order? AffectedOrder { get; set; }
    }
}
