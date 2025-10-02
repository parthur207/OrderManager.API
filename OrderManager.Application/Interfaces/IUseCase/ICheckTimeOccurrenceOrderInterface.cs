using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IUseCase
{
    public interface ICheckTimeOccurrenceOrderInterface
    {
        Task<SimpleResponseModel> CheckTime(OrderNumberVO OrderNumber, ETypeOccurrenceEnum typeOccurrence);
    }
}
