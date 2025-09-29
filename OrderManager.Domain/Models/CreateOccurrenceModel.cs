using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
    public class CreateOccurrenceModel
    {

        public int OrderNumber { get; set; }

        public ETypeOccurrenceEnum ETypeOccurrence { get; set; }
    }
}
