using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IServices.ICommandsGenericUser;
using OrderManager.Application.Interfaces.IServices.IQueriesGenericUser;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.Queries
{
    public class OrderQueriesUserGenericService : IOrderQueriesUserGenericInterface
    {
        public Task<ResponseModel<List<OrderDTO>>?> GetOrdersByUserId(int userId)
        {
            
        }
    }
}
