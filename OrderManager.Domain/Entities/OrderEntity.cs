using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
        public OrderEntity() 
        {
            TimeOrder=DateTime.Now;
        }

        public int OrderNumber { get; private set; }
        public List<OcccurrenceEntity> Occurrences { get; private set; }
        public DateTime TimeOrder { get; private set; }
        public bool IndDelivered { get; private set; }

        //Métodos de domínio
        public void SetStatusToDelivered()
        {
            IndDelivered = true;
            UpdatedAt=DateTime.Now;
        }
    }
}
