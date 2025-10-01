using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers
{
    public class OrderMapper : IOrderMapperInterface
    {
        public ResponseModel<OrderEntity> OrderCreateModelToEntity(CreateOrderModel OrderModel)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<OrderDTO>? OrderEntityToDTO(OrderEntity OrderEntity)
        {
            throw new NotImplementedException();
        }
    }
}
