using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.ICommandsGenericUser
{
    public interface IOrderCommandsUserGenericInterface
    { 
        SimpleResponseModel CreateOrder(CreateOrderModel Model);
    }
}
