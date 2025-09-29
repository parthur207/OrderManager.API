using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class OcccurrenceEntity : BaseEntity
    {
        public OcccurrenceEntity() { }
        public ETypeOccurrenceEnum TypeOccurrence { get; private set; }
        public DateTime TimeOccurrence { get; private set; }
        public bool IndFinishing { get; private set; }

    }
}
