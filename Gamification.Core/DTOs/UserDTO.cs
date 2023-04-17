using Gamification.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.DTOs
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email!;
            Phone = user.PhoneNumber!;
            Type = user.Type;
            Status = user.Status;
            Points = user.Points!;
        }

        public UserDTO(StandardUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email!;
            Phone = user.PhoneNumber!;
            Type = user.Type;
            SectorId = user.SectorId;
            Points = user.Points!;
        }

        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserType Type { get; set; } = UserType.Standard;
        public UserStatus Status { get; set; } = 0;
        public string StatusString
        {
            get => Status switch
            {
                UserStatus.Active => "Ativo",
                UserStatus.Inactive => "Inativo",
                _ => "Sem registros"
            };
        }
        public int Points { get; set; } = 0;
        public Guid? SectorId { get; set; }
        public string TypeString
        {
            get => Type switch
            {
                UserType.Administrator => "Administrador",
                UserType.Supervisor => "Supervisor",
                UserType.Standard => "Padrão",
                _ => "Sem registros"
            };
        }
    }
}
