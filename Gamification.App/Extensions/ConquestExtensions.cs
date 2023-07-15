using Gamification.Core.DTOs;
using Gamification.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gamification.App.Models.ConquestModels;

namespace Gamification.App.Extensions
{
    public static class ConquestExtensions
    {
        public static IQueryable<Conquest> PersonalFilterToUser(this IQueryable<Conquest> items, ConquestFilterModel filter)
        {
            items = items.Include(x => x.Users);
            items = String.IsNullOrEmpty(filter.Title) ? items.Where(x => x.Title.ToLower().Contains(filter.Title!.ToLower())) : items;
            items = filter.Concluded == false && filter.UserId != null ? items.Where(x => !x.Users.Select(x => x.UserId).Contains(filter.UserId)) : items;
            items = filter.Concluded == true && filter.UserId != null ? items.Where(x => x.Users.Select(x => x.UserId).Contains(filter.UserId)) : items;

            return items;
        }

        public static IQueryable<Conquest> PersonalFilterToSector(this IQueryable<Conquest> items, ConquestFilterModel filter)
        {
            items = items.Include(x => x.Sectors);
            items = String.IsNullOrEmpty(filter.Title) ? items.Where(x => x.Title.ToLower().Contains(filter.Title!.ToLower())) : items;
            items = filter.Concluded == false && filter.SectorId != null ? items.Where(x => !x.Sectors.Select(x => x.SectorId).ToList().Contains((Guid) filter.SectorId)) : items;
            items = filter.Concluded == true && filter.SectorId != null ? items.Where(x => x.Sectors.Select(x => x.SectorId).ToList().Contains((Guid) filter.SectorId)) : items;

            return items;
        }

        public static IQueryable<ConquestDTO> MapToDTO(this IQueryable<Conquest> items)
        {
            return items.Select(x => new ConquestDTO(x));
        }
    }
}
