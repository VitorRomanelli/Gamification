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
    public static class SectorExtensions
    {
        public static IQueryable<SectorDTO> MapToDTO(this IQueryable<Sector> items)
        {
            return items.Select(x => new SectorDTO(x));
        }
    }
}
