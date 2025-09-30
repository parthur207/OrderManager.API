using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Commands
{
    public interface IOrderCommandsRepository
    {

        Task<SimpleResponseModel> CreateOrder(OrderEntity Entity);
        Task<SimpleResponseModel> UpdateStatusOrder(OrderEntity Entity);

        Task<SimpleResponseModel> CreateOrderOccurrence(OrderEntity entity);
    }
}
