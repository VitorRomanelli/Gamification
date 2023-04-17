using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class SectorConquest
    {
        public Guid Id { get; set; }
        public Guid SectorId { get; set; }
        public Sector? Sector { get; set; }
        public Guid ConquestId { get; set; }
        public Conquest? Conquest { get; set; }
    }
}
