using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class Sector
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string SupervisorId { get; set; } = string.Empty;
        public SupervisorUser? Supervisor { get; set; }

        public List<StandardUser> Users { get; set; } = new List<StandardUser>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public int Points
        {
            get
            {
                var points = 0;

                if(Users != null && Users.Any())
                {
                    points += Users.Sum(x => x.Points);
                }

                if(Supervisor != null)
                {
                    points += Supervisor.Points;
                }

                return points;
            }
        }

        public int? ConcludedOrders
        {
            get
            {
                if (Orders != null && Orders.Any())
                {
                    return Orders.Count(x => x.Step == Step.Done);
                }
                return null;
            }
        }
    }
}
