using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
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
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByUserEmail(UserEmailVO email);
        Task<ResponseModel<OrderEntity>?> GetOrderById(OrderNumberVO OrderNumber);
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrenceEnum);
    }
}
