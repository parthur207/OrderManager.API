using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models.ReponsePattern
{
    public class ResponseModel<T>
    {
        public ResponseModel() { }

        public T? Content { get; set; }
        public string? Message { get; set; }
        public ResponseStatusEnum Status { get; set; }

    }
}
