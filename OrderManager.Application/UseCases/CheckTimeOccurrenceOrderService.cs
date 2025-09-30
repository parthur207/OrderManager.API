using OrderManager.Application.Interfaces.IUseCase;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.UseCases
{
    public class CheckTimeOccurrenceOrderService : ICheckTimeOccurrenceOrderInterface
    {

        private readonly ICheckTimeOccurrenceOrderRespository _checkTimeOccurrenceOrderRespository;

        public CheckTimeOccurrenceOrderService(ICheckTimeOccurrenceOrderRespository checkTimeOccurrenceOrderRespository)
        {
            _checkTimeOccurrenceOrderRespository = checkTimeOccurrenceOrderRespository;
        }
        public SimpleResponseModel CheckTime(int OrderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
