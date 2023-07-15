using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions;
using Gamification.Core.Entities;

namespace Gamification.App.Models
{
    public class ConquestModels
    {
        public class ConquestFilterModel : ICustomQueryable
        {
            public int Page { get; set; }
            public int PageSize { get; set; } = 10;

            [QueryOperator(Operator = WhereOperator.Contains)]
            public string? Title { get; set; } = string.Empty;

            public string? UserId { get; set; } = string.Empty;
            public Guid? SectorId { get; set; }
            public bool? Concluded { get; set; }

            public ConquestType? Type { get; set; }
        }

        public class ConquestAddModel
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;

            public int? Points { get; set; }
            public int? ServicesConcludedCount { get; set; }

            public DateTime? EndDate { get; set; }

            public ConquestTo To { get; set; }
            public ConquestType Type { get; set; }
        }

        public class ConquestEditModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;

            public int? Points { get; set; }
            public int? ServicesConcludedCount { get; set; }

            public DateTime? EndDate { get; set; }

            public ConquestTo To { get; set; }
            public ConquestType Type { get; set; }
        }
    }
}
