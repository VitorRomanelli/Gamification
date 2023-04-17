using Gamification.App.Models;
using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<UserDTO> MapToDTO(this IQueryable<User> items)
        {
            return items.Select(x => new UserDTO(x));
        }

        public static IQueryable<UserDTO> MapToDTO(this IQueryable<StandardUser> items)
        {
            return items.Select(x => new UserDTO(x));
        }
    }
}
