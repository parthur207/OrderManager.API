using OrderManager.Application.Interfaces.IServices.Adm;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Application.RepositoryInterface.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.Commands
{
    public class OrderCommandsService : IOrderCommandsAdmInterface
    {
        private readonly IOrderCommandsRepository _orderCommandsRepository;
        private readonly IOrderMapperInterface _orderMapper;
        public OrderCommandsService(IOrderCommandsRepository orderCommandsRepository, IOrderMapperInterface orderMapper)
        {
            _orderCommandsRepository = orderCommandsRepository;
            _orderMapper = orderMapper;
        }
    }
}
