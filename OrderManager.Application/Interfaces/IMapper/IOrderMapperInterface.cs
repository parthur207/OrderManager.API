using OrderManager.Application.DTOs;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IMapper
{
    public interface IOrderMapperInterface
    {
        //Entity=>DTO
        ResponseModel<OrderDTO>? OrderEntityToDTO(OrderEntity OrderEntity);
        ResponseModel<List<OrderDTO>>? OrderEntityListToDTOList(List<OrderEntity> OrderEntityList);

        //Model=>Entity
        ResponseModel<OrderEntity> OrderCreateModelToEntity(int GeneratedNumber, int userId);

    }
}
