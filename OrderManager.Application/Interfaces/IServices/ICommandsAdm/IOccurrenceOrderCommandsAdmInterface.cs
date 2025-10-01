using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.ICommandsAdm
{
    public interface IOccurrenceOrderCommandsAdmInterface
    {

        Task<SimpleResponseModel> CreateOccurrenceToOrder(CreateOrderModel orderNumber);
        Task<SimpleResponseModel> DeleteOccurrenceByOrderNumber(OrderNumberVO OrderNumber, int OccurrenceId);

    }
}
