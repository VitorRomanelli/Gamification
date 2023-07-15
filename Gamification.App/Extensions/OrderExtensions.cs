using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gamification.App.Models.OrderModels;

namespace Gamification.App.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<Order> ApplyFilter(this IQueryable<Order> items, OrderFilterModel filter)
        {
            items = items.AsNoTracking();
            items = filter.SectorId != null ? items.Where(x => x.SectorId == filter.SectorId) : items;
            items = !String.IsNullOrEmpty(filter.Title) ? items.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower())) : items;
            return items;
        }

        public static IQueryable<OrderDTO> MapToDTO(this IQueryable<Order> items)
        {
            return items.Select(x => new OrderDTO(x));
        }
    }
}
