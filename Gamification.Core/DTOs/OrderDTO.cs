using Gamification.Core.Entities;

namespace Gamification.Core.DTOs
{
    public class OrderDTO
    {
        public OrderDTO(Order order)
        {
            Id = order.Id;
            Title = order.Title;
            Description = order.Description;
            Step= order.Step;
            SectorId = order.SectorId;
            RewardPoints = order.RewardPoints;
            Sector = order.Sector != null ? new SectorDTO(order.Sector!) : null;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RewardPoints { get; set; } = 0;
        public Step Step { get; set; } = Step.Schedule;
        public Guid SectorId { get; set; }
        public SectorDTO? Sector { get; set; }
        public string StepString
        {
            get => Step switch
            {
                Step.Schedule => "Agendado",
                Step.InProcess => "Em progresso",
                Step.Done => "Feito",
                _ => "Sem registros"
            };
        }
    }
}
