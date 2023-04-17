using Gamification.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.DTOs
{
    public class SectorDTO
    {
        public SectorDTO(Sector entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            SupervisorId = entity.SupervisorId;
            Supervisor = entity.Supervisor != null ? new UserDTO(entity.Supervisor) : null;
            Users = entity.Users.Select(x => new UserDTO(x)).ToList();
            Orders = entity.Orders;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string SupervisorId { get; set; } = string.Empty;
        public UserDTO? Supervisor { get; set; }

        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
        public List<Order> Orders { get; set; } = new List<Order>();

        [NotMapped]
        public int Points
        {
            get
            {
                if (Users != null && Users.Any())
                {
                    return Users.Sum(x => x.Points);
                }
                return 0;
            }
        }
    }
}
