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
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersRepository();
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByUserEmailRepository(UserEmailVO email);
        Task<ResponseModel<OrderEntity>?> GetOrderByNumberRepository(OrderNumberVO OrderNumber);
        Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByTypeOccurrenceRepository(ETypeOccurrenceEnum occurrenceEnum);
    }
}
