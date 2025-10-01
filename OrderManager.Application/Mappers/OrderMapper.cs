using OrderManager.Application.DTOs;
using OrderManager.Application.DTOs.Adm;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers
{
    public class OrderMapper : IOrderMapperInterface
    {
        private readonly IOccurrenceMapperInterface _occurrenceMapperInterface;
        public OrderMapper(IOccurrenceMapperInterface occurrenceMapperInterface)
        {
            _occurrenceMapperInterface = occurrenceMapperInterface;
        }
        public ResponseModel<OrderEntity> OrderCreateModelToEntity(CreateOrderModel OrderModel, int userId)
        {
            ResponseModel<OrderEntity> Response= new ResponseModel<OrderEntity>();
            try
            {
                if (OrderModel is null)
                {
                    Response.Message = "O modelo de pedido não pode ser nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                var orderEntityConverted = new OrderEntity
                (
                    new OrderNumberVO(OrderModel.OrderNumber),
                    userId
                );

                Response.Content = orderEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message ="Ocorreu um erro inesperado: "+ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }

        public ResponseModel<OrderDTO> OrderEntityToDTO(OrderEntity OrderEntity)
        {
            ResponseModel<OrderDTO> Response = new ResponseModel<OrderDTO>();
            try
            {
                if (OrderEntity is null)
                {
                    Response.Message = "O modelo de pedido não pode ser nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                
                var orderDTO = new OrderDTO
                (
                    OrderEntity.OrderNumber.Value, 
                    _occurrenceMapperInterface.MapToOccurrenceDTOList(OrderEntity.Occurrences).Content, 
                    OrderEntity.User.Name, 
                    OrderEntity.User.Email.Value,
                    OrderEntity.User.Address,
                    OrderEntity.IndDelivered,
                    OrderEntity.TimeOrder
                );

                Response.Content = ;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }
    }
}
