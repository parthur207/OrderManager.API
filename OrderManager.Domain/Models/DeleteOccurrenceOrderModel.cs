using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
    public class DeleteOccurrenceOrderModel
    {
        public int OrderNumber { get; set; }
        public int OccurrenceId { get; set; }
    }
}
