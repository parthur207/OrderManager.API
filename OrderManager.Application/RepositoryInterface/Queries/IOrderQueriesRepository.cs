using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Queries
{
    public interface IOrderQueriesRepository
    {
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrders();
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByUserEmail(string email);
        Task<ResponseModel<OrderEntity>?> GetOrderById(int OrderNumber);
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrenceEnum);
    }
}
