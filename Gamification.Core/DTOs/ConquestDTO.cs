using Gamification.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.DTOs
{
    public class ConquestDTO
    {
        public ConquestDTO(Conquest conquest)
        {
            Id = conquest.Id;
            Title = conquest.Title;
            Description = conquest.Description;
            Points = conquest.Points;
            ServicesConcludedCount = conquest.ServicesConcludedCount;
            EndDate = conquest.EndDate;
            To = conquest.To;
            Type = conquest.Type;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? Points { get; set; }
        public int? ServicesConcludedCount { get; set; }

        public DateTime? EndDate { get; set; }

        public ConquestTo To { get; set; }
        public ConquestType Type { get; set; }


        public string ComputedTo
        {
            get => To switch
            {
                ConquestTo.User => "Usuário",
                ConquestTo.Sector => "Setor",
                _ => "Sem registros"
            };
        }

        public string ComputedType {
            get => Type switch
            {
                ConquestType.Points => "Pontos",
                ConquestType.ServicesConcluded => "Serviços concluídos",
                _ => "Sem registros"
            };
        }
    }
}
