using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Commands
{
    public interface IOccurrenceOrderCommandsRepository
    {
        Task<SimpleResponseModel> CreateOccurrenceToOrderRepository(OrderNumberVO orderNumber, OccurrenceEntity occurrenceEntity);
        Task<SimpleResponseModel> DeleteOccurrenceByOrderNumberRepository(OrderNumberVO OrderNumber, int OccurrenceId);
    }
}
