using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.IQueriesUserCommon;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;

namespace OrderManager.Application.Services.QueriesUserCommon
{
    public class OrderQueriesUserCommonService : IOrderQueriesUserCommonInterface
    {
        private readonly IOrderQueriesRepository _orderQueriesRepository;
        private readonly IOrderMapperInterface _orderMapperInterface;
        public OrderQueriesUserCommonService(IOrderQueriesRepository orderQueriesRepository, IOrderMapperInterface orderMapperInterface)
        {
            _orderQueriesRepository = orderQueriesRepository;
            _orderMapperInterface = orderMapperInterface;
        }
        public async Task<ResponseModel<List<OrderDTO>?>> GetOrdersByUserId(int userId)
        {
            ResponseModel<List<OrderDTO>?> Response = new ResponseModel<List<OrderDTO>?>();
            try
            {
                var orderEntity = await _orderQueriesRepository.GetAllOrdersByUserIdRepository(userId);

                if (orderEntity.Status.Equals(ResponseStatusEnum.NotFound)
                    || orderEntity.Status.Equals(ResponseStatusEnum.Error)
                    || orderEntity.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = orderEntity.Status;
                    Response.Message = orderEntity.Message;

                    return Response;
                }
                var orderDTO = _orderMapperInterface.OrderEntityListToDTOList(orderEntity.Content);

                Response.Content = orderDTO.Content;
                Response.Status = orderDTO.Status;
                Response.Message = orderDTO.Message;
            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }
    }
}
