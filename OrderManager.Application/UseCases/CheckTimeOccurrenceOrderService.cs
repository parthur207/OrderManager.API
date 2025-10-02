using OrderManager.Application.Interfaces.IUseCase;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
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
        public async Task<SimpleResponseModel> CheckTime(OrderNumberVO OrderNumber, ETypeOccurrenceEnum typeOccurrence)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                if (OrderNumber is null)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O número do pedido é nulo.";
                    return Response;
                }
                
             
                var ResponseRepository = await _checkTimeOccurrenceOrderRespository.CheckTimeRepository(OrderNumber.Value, typeOccurrence);

                if (ResponseRepository.Status.Equals(ResponseStatusEnum.Error)
                    || ResponseRepository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= ResponseRepository.Status;
                    Response.Message = ResponseRepository.Message;
                    return Response;
                }
            }
            catch (Exception ex)
            {
                Response.Status = Domain.Enuns.ResponseStatusEnum.Error;
                Response.Message = ex.Message;
            }
            return Response;
        }
    }
}
