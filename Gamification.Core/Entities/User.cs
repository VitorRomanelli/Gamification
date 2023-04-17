using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamification.Core.Entities
{
    public class User : IdentityUser<string>
    {
        [NotMapped]
        public string Passsword { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Points { get; set; } = 0;

        public UserStatus Status { get; set; } = UserStatus.Active;

        public UserType Type { get; set; } = UserType.Standard;

        public List<UserConquest> Conquests { get; set; } = new List<UserConquest>();
    }

    public enum UserType 
    {
        [Description("Administrador")]
        Administrator,
        [Description("Supervisor")]
        Supervisor,
        [Description("Padrão")]
        Standard,
    }

    public enum UserStatus
    {
        Active,
        Inactive,
    }
}
