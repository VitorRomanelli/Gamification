namespace Gamification.Core.Entities
{
    public class Conquest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public int? Points { get; set; }
        public int? ServicesConcludedCount { get; set; }

        public DateTime? EndDate { get; set; }

        public ConquestTo To { get; set; }
        public ConquestType Type { get; set; }

        public List<UserConquest> Users = new();
        public List<SectorConquest> Sectors = new();
    }

    public enum ConquestTo
    {
        User,
        Sector,
    }

    public enum ConquestType
    {
        Points,
        ServicesConcluded,
    }
}
