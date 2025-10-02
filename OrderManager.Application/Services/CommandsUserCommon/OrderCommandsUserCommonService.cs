using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;

namespace OrderManager.Application.Services.CommandsUserGeneric
{
    public class OrderCommandsUserCommonService : IOrderCommandsUserCommonInterface
    {
        private readonly IOrderCommandsRepository _orderCommandsRepository;
        private readonly IOrderMapperInterface _orderMapperInterface;
        public OrderCommandsUserCommonService(IOrderCommandsRepository orderCommandsRepository, 
            IOrderMapperInterface orderMapperInterface)
        {
            _orderCommandsRepository = orderCommandsRepository;
            _orderMapperInterface = orderMapperInterface;
        }
        public async Task<SimpleResponseModel> CreateOrder(int GeneratedNumber, int UserId)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                if (GeneratedNumber>9999)
                {
                    Response.Status= ResponseStatusEnum.Error;
                    Response.Message = "Erro. O modelo de criação de pedido não pode ter mais de 4 digitos.";
                    return Response;
                }

                var OrderEntityConverted = _orderMapperInterface.OrderCreateModelToEntity(GeneratedNumber, UserId);

                if (OrderEntityConverted.Status.Equals(ResponseStatusEnum.Error) 
                    || OrderEntityConverted.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= OrderEntityConverted.Status;
                    Response.Message = OrderEntityConverted.Message;
                }
                var ResponseRespository = await _orderCommandsRepository.CreateOrderRepository(OrderEntityConverted.Content);
                
                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }
                Response.Status= ResponseRespository.Status;
                Response.Message = ResponseRespository.Message;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError; 
                Response.Message= "Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }
    }
}
