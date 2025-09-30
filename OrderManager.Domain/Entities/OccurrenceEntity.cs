using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class OccurrenceEntity : BaseEntity
    {
        public OccurrenceEntity() 
        {
            TimeOccurrence=DateTime.Now;
            IndFinishing = false;
        }
        public ETypeOccurrenceEnum TypeOccurrence { get; private set; }
        public DateTime TimeOccurrence { get; private set; }
        public bool IndFinishing { get; private set; }

        public int OrderId { get; private set; }//chave estangeira
        public OrderEntity Order { get; private set; }

        public void SetOccurrenceToFinishing()//métdo de domínio
        {
            IndFinishing=true;
            UpdatedAt = DateTime.Now;
        }
    }
}
