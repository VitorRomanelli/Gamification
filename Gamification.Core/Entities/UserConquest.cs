using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class UserConquest
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public Guid ConquestId { get; set; }
        public Conquest? Conquest { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
