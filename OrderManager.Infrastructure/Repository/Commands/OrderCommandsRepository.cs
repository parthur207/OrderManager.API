using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OrderManager.Application.Interfaces.IUseCase;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.Commands
{
    public class OrderCommandsRepository : IOrderCommandsRepository
    {

        private readonly DbContextOrderManager _dbContextOM;
        public OrderCommandsRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM=dbContextOM;
        }

        public async Task<SimpleResponseModel> CreateOrderRepository(OrderEntity Entity)
        {
            SimpleResponseModel Response=new SimpleResponseModel();

            try
            {
                int orderNumberValue = Entity.OrderNumber.Value; 

                if (await _dbContextOM.OrderEntity.AnyAsync(x=>x.OrderNumber.Value== orderNumberValue))
                {
                    Response.Message = $"Erro. Já existe um pedido com o número informado: {orderNumberValue}";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                await _dbContextOM.OrderEntity.AddAsync(Entity);
                await _dbContextOM.SaveChangesAsync();

                Response.Status=ResponseStatusEnum.Success;
                Response.Message = $"Pedido criado com sucesso!\nDetalhes: {orderNumberValue} | {Entity.TimeOrder}";
            }
            catch(Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message="Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }

       

    }
}
