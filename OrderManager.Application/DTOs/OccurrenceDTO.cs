using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTOs
{
    public class OccurrenceDTO
    {
        public OccurrenceDTO(ETypeOccurrenceEnum typeOccurrence, DateTime timeOccurrence, bool indFinishing)
        {
            TypeOccurrence = typeOccurrence;
            TimeOccurrence = timeOccurrence;
            IndFinishing = indFinishing;
        }

        public ETypeOccurrenceEnum TypeOccurrence { get; set; }
        public DateTime TimeOccurrence { get; set; }
        public bool IndFinishing { get; set; }
    }
}
