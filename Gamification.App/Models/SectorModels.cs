using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Models
{
    public class SectorFilterModel : ICustomQueryable
    { 
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;

        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Name { get; set; } = string.Empty;
    }

    public class SectorAddModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SupervisorId { get; set; } = string.Empty;
    }

    public class SectorEditModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SupervisorId { get; set; } = string.Empty;
    }
}
