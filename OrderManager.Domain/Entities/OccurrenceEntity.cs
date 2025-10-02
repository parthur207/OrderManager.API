using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class OccurrenceEntity : BaseEntity
    {
        public OccurrenceEntity(int orderNumber, ETypeOccurrenceEnum typeOccurrence) 
        {
            OrderNumber = orderNumber;
            TypeOccurrence=typeOccurrence;
            TimeOccurrence =DateTime.Now;
            IndFinishing = false;
        }

        public OccurrenceEntity(int orderNumber, int OccurrenceId)
        {
            OrderNumber = orderNumber;
            Id = OccurrenceId;
        }

        public ETypeOccurrenceEnum TypeOccurrence { get; private set; }
        public DateTime TimeOccurrence { get; private set; }
        public bool IndFinishing { get; private set; }
        public int OrderNumber { get; private set; }//chave estangeira
        public OrderEntity Order { get; private set; }

        public void SetOccurrenceToFinishing()//métdo de domínio
        {
            IndFinishing=true;
            UpdatedAt = DateTime.Now;
        }
    }
}
