using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDTO> MapToDTO(this IQueryable<Order> items)
        {
            return items.Select(x => new OrderDTO(x));
        }
    }
}
