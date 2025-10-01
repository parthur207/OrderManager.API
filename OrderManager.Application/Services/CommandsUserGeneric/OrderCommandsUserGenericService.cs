using OrderManager.Application.Interfaces.IServices.ICommandsGenericUser;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.CommandsUserGeneric
{
    public class OrderCommandsUserGenericService : IOrderCommandsUserGenericInterface
    {
        public async Task<SimpleResponseModel> CreateOrder(CreateOrderModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
