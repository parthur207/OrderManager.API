using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;
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
            TimeOrder = DateTime.Now;
        }
        public OrderNumberVO OrderNumber { get; private set; }
        public List<OccurrenceEntity> Occurrences { get; private set; }
        public int UserId { get; private set; }
        public UserEntity User { get; private set; } 
        public DateTime TimeOrder { get; private set; }
        public bool IndDelivered { get; private set; }

        //Métodos de domínio
        public void SetOrderStatusToDelivered()
        {
            IndDelivered = true;
            UpdatedAt=DateTime.Now;
        }

        public void AddOccurrenceToOrder(OccurrenceEntity occurrence)
        {
            if (Occurrences is null)
                Occurrences = new List<OccurrenceEntity>();

            Occurrences.Add(occurrence);
            UpdatedAt = DateTime.Now;
        }

        public void DeleteOccurrenceFromOrder(OccurrenceEntity occurrence)
        {
            Occurrences.Remove(occurrence);
            UpdatedAt= DateTime.Now;
        }
    }
}
