using OrderManager.Application.DTOs;
using OrderManager.Application.DTOs.Adm;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Interfaces.IMapper;
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
        public ResponseModel<OrderEntity> OrderCreateModelToEntity(int GeneratedNumber, int userId)
        {
            ResponseModel<OrderEntity> Response= new ResponseModel<OrderEntity>();
            try
            {
                if (GeneratedNumber<1000)
                {
                    Response.Message = "Erro. O número do pedido deve ter pelo menos 4 digitos.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                var orderEntityConverted = new OrderEntity
                (
                    new OrderNumberVO(GeneratedNumber),
                    userId
                );

                if (orderEntityConverted is null)
                {
                    Response.Status= ResponseStatusEnum.Error;
                    Response.Message = "O pedido deve ter exatamente 4 digitos.";
                    return Response;
                }

                Response.Content = orderEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.Error;
                Response.Message =ex.Message;
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
                
                var orderDTOConverted = new OrderDTO
                (
                    OrderEntity.OrderNumber.Value, 
                    _occurrenceMapperInterface.OccurrencesEntityListToDTOList(OrderEntity.Occurrences).Content, 
                    OrderEntity.User.Name, 
                    OrderEntity.User.Email.Value,
                    OrderEntity.User.Address,
                    OrderEntity.IndDelivered,
                    OrderEntity.TimeOrder
                );

                Response.Content = orderDTOConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message =  ex.Message;            }
            return Response;
        }

       public ResponseModel<List<OrderDTO>>? OrderEntityListToDTOList(List<OrderEntity> OrderEntityList)
        {
            ResponseModel<List<OrderDTO>?> Response = new ResponseModel<List<OrderDTO>>();
            try
            {
                if (OrderEntityList is null || !OrderEntityList.Any())
                {
                    Response.Message = "Lista de pedidos vazia.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                foreach (var item in OrderEntityList)
                {
                    var OrderDTOConverted= new OrderDTO
                        (
                            item.OrderNumber.Value,
                            _occurrenceMapperInterface.OccurrencesEntityListToDTOList(item.Occurrences).Content,
                            item.User.Name,
                            item.User.Email.Value,
                            item.User.Address,
                            item.IndDelivered,
                            item.TimeOrder
                        );

                    Response.Content.Add(OrderDTOConverted);
                }

                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message =  ex.Message;
            }
            return Response;
        }
    }
}
