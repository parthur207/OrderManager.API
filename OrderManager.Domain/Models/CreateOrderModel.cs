using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Domain.Models
{
    public class CreateOrderModel
    {
        [Required(ErrorMessage ="Erro. É necessário o informe do número do pedido para prosseguir com sua criação.")]
        public int OrderNumber { get; set; }

    }
}
