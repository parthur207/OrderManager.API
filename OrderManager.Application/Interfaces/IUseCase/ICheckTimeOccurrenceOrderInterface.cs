using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IUseCase
{
    public interface ICheckTimeOccurrenceOrderInterface
    {
        SimpleResponseModel CheckTime(int OrderNumber);
    }
}
