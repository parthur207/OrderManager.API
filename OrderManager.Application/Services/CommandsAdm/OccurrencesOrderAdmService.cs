using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.CommandsAdm
{
    public class OccurrencesOrderAdmService : IOccurrenceOrderCommandsAdmInterface
    {
        public Task<SimpleResponseModel> CreateOccurrenceToOrder(CreateOrderModel orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleResponseModel> DeleteOccurrenceByOrderNumber(OrderNumberVO OrderNumber, int OccurrenceId)
        {
            throw new NotImplementedException();
        }
    }
}
