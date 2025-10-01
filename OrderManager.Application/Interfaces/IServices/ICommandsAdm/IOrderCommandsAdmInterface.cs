using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.ICommandsAdm
{
    public interface IOrderCommandsAdmInterface
    {

        SimpleResponseModel InactiveOrder(OrderNumberVO orderId);
    }
}
