using OrderManager.Application.DTOs;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.IQueriesGenericUser
{
    public interface IOrderQueriesUserGenericInterface
    {

        Task<ResponseModel<List<OrderDTO>>?> GetOrdersByUserId(int userId);
    }
}
