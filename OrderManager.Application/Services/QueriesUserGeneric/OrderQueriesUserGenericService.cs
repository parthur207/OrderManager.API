using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IServices.ICommandsGenericUser;
using OrderManager.Application.Interfaces.IServices.IQueriesGenericUser;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.Queries
{
    public class OrderQueriesUserGenericService : IOrderQueriesUserGenericInterface
    {
        private readonly IOrderQueriesRepository _orderQueriesRepository;
        private readonly IOrderMapperInterface _orderMapperInterface;
        public OrderQueriesUserGenericService(IOrderQueriesRepository orderQueriesRepository, IOrderMapperInterface orderMapperInterface)
        {
            _orderQueriesRepository = orderQueriesRepository;
            _orderMapperInterface = orderMapperInterface;
        }
        public async Task<ResponseModel<List<OrderDTO>?>> GetOrdersByUserId(int userId)
        {
            ResponseModel<List<OrderDTO>?> Response= new ResponseModel<List<OrderDTO>?>();
            try
            {
                var orderEntity = await _orderQueriesRepository.GetAllOrdersByUserIdRepository(userId);

                if (orderEntity.Status.Equals(ResponseStatusEnum.NotFound)
                    || orderEntity.Status.Equals(ResponseStatusEnum.Error)
                    || orderEntity.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= orderEntity.Status;
                    if (orderEntity.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = orderEntity.Message;

                    return Response;
                }
                var orderDTO = _orderMapperInterface.OrderEntityListToDTOList(orderEntity.Content);

                Response.Content = orderDTO.Content;
                Response.Status = orderDTO.Status;
                Response.Message = orderDTO.Message;
            }
            catch(Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }
    }
}
