using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Commands
{
    public interface IOccurrenceOrderCommandsRepository
    {
        Task<SimpleResponseModel> CreateOccurrenceToOrder(int orderNumber, OccurrenceEntity occurrenceEntity);
        Task<SimpleResponseModel> DeleteOccurrenceByOrderNumber(int OrderNumber, int OccurrenceId);
    }
}
