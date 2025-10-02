using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Application.Services.QueriesAdm
{
    public class OrderQueriesAdmService : IOrderQueriesAdmInterface
    {
        private readonly IOrderQueriesRepository _orderQueriesRepository;
        private readonly IOrderMapperInterface _orderMapperInterface;
        public OrderQueriesAdmService(IOrderQueriesRepository orderQueriesRepository,
            IOrderMapperInterface orderMapperInterface)
        {
            _orderQueriesRepository = orderQueriesRepository;
            _orderMapperInterface = orderMapperInterface;
        }
        public async Task<ResponseModel<List<OrderDTO>?>> GetAllOrders()
        {
            ResponseModel<List<OrderDTO>?> Response = new ResponseModel<List<OrderDTO>>();
            try
            {
                var ResponseRespository = await _orderQueriesRepository.GetAllOrdersRepository();

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    if (ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = ResponseRespository.Message;

                    return Response;
                }
                var OrdersDTO= _orderMapperInterface.OrderEntityListToDTOList(ResponseRespository.Content);

                Response.Status = OrdersDTO.Status;
                Response.Message = OrdersDTO.Message;
                Response.Content = OrdersDTO.Content;
            }
            catch (Exception ex)
            {
                //Log de erro
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado.";
            }
            return Response;

        }

        public async Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrence)
        {
            ResponseModel<List<OrderDTO>?> Response = new ResponseModel<List<OrderDTO>>();
            try
            {
                var ResponseRespository = await _orderQueriesRepository.GetAllOrdersByTypeOccurrenceRepository(occurrence);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    if (ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = ResponseRespository.Message;

                    return Response;
                }

                var OrdersDTO = _orderMapperInterface.OrderEntityListToDTOList(ResponseRespository.Content);

                Response.Status = OrdersDTO.Status;
                Response.Message = OrdersDTO.Message;
                Response.Content = OrdersDTO.Content;
                return Response;
            }
            catch (Exception ex)
            {
                //Log de erro
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado.";
            }
            return Response;

        }

        public async Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByUserEmail(UserEmailVO Email)
        {
            ResponseModel<List<OrderDTO>?> Response = new ResponseModel<List<OrderDTO>>();
            try
            {
                var ResponseRespository = await _orderQueriesRepository.GetAllOrdersByUserEmailRepository(Email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    if (ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = ResponseRespository.Message;

                    return Response;
                }

                var OrdersDTO = _orderMapperInterface.OrderEntityListToDTOList(ResponseRespository.Content);
                
                Response.Status = OrdersDTO.Status;
                Response.Message = OrdersDTO.Message;
                Response.Content = OrdersDTO.Content;
            }
            catch (Exception ex)
            {
                //Log
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado.";
            }
            return Response;

        }

        public async Task<ResponseModel<OrderDTO>?> GetOrderByNumber(OrderNumberVO orderNumber)
        {
            ResponseModel<OrderDTO>? Response = new ResponseModel<OrderDTO>();
            try
            {
                var ResponseRespository = await _orderQueriesRepository.GetOrderByNumberRepository(orderNumber);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    if (ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = ResponseRespository.Message;

                    return Response;
                }
                var OrderDTO = _orderMapperInterface.OrderEntityToDTO(ResponseRespository.Content);
                
                Response.Status = OrderDTO.Status;
                Response.Message = OrderDTO.Message;
                Response.Content = OrderDTO.Content;
            }
            catch (Exception ex)
            {
                //Log de erro
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado.";
            }
            return Response;

        }
    }
}
