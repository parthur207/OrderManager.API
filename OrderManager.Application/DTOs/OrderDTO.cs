using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTOs
{
    public class OrderDTO
    {
        public OrderDTO(int orderNumber, List<OccurrenceDTO> occurrences, string requesterUserName, 
            string requesterUserEmail, string deliveryAddress, bool indDelivered, DateTime timeOrder)
        {
            OrderNumber = orderNumber;
            Occurrences = occurrences;
            RequesterUserName = requesterUserName;
            RequesterUserEmail = requesterUserEmail;
            DeliveryAddress = deliveryAddress;
            IndDelivered = indDelivered;
            TimeOrder = timeOrder;
        }

        public int OrderNumber { get; set; }
        public List<OccurrenceDTO> Occurrences { get; set; }
        public string RequesterUserName { get; set; }
        public string RequesterUserEmail { get; set; }
        public string DeliveryAddress {  get; set; }
        public bool IndDelivered { get; set; }
        public DateTime TimeOrder { get; private set; }
        
    }
}
