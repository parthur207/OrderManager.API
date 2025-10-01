using OrderManager.Application.DTOs;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers.MappersInterface
{
    public interface IOrderMapperInterface
    {
        //Entity=>DTO
        ResponseModel<OrderDTO>? OrderEntityToDTO(OrderEntity OrderEntity);

        //Model=>Entity
        ResponseModel<OrderEntity> OrderCreateModelToEntity(CreateOrderModel OrderModel, int userId);

    }
}
