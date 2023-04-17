namespace Gamification.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Step Step { get; set; } = Step.Schedule;

        public int RewardPoints { get; set; } = 0;

        public Guid SectorId { get; set; }
        public Sector? Sector { get; set; }
    }

    public enum Step
    {
        Schedule,
        InProcess,
        Done
    }
}
