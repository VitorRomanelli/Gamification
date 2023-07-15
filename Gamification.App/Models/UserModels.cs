using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Gamification.Core.Entities;

namespace Gamification.App.Models
{
    public class UserFilterModel : ICustomQueryable
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;

        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Name { get; set; } = string.Empty;
    }

    public class UserAddModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid? SectorId { get; set; }
        public UserType Type { get; set; }
    }

    public class ChangeStatusModel
    {
        public string Id { get; set; }
        public UserStatus Status { get; set; }
    }

    public class UserEditModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid? SectorId { get; set; }
        public UserType Type { get; set; }

        public string? Picture { get; set; }
        public string? Extension { get; set; }
    }
}
