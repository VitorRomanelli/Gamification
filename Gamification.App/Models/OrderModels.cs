using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions;
using Gamification.Core.Entities;

namespace Gamification.App.Models
{
    public class OrderModels
    {
        public class OrderFilterModel : ICustomQueryable
        {
            public int Page { get; set; }
            public int PageSize { get; set; } = 10;

            [QueryOperator(Operator = WhereOperator.Contains)]
            public string? Title { get; set; } = string.Empty;
        }

        public class OrderAddModel
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int RewardPoints { get; set; } = 0;
            public Step Step { get; set; } = Step.Schedule;
            public Guid SectorId { get; set; }
        }

        public class OrderEditModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int RewardPoints { get; set; } = 0;
            public Guid SectorId { get; set; }
        }
    }
}
