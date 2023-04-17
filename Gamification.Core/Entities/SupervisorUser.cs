using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class SupervisorUser : User
    {
        public Sector? Sector { get; set; }
    }
}
