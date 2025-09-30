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
    public class OrderCommandsRespository : IOrderCommandsRepository
    {

        private readonly DbContextOrderManager _dbContextOM;
        public OrderCommandsRespository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM=dbContextOM;
        }

        public  async Task<SimpleResponseModel> CreateOrder(OrderEntity Entity)
        {
            SimpleResponseModel Response=new SimpleResponseModel();

            try
            {
                if (await _dbContextOM.OrderEntity.AnyAsync(x=>x.OrderNumber==Entity.OrderNumber))
                {
                    Response.Message = $"Erro. Já existe um pedido com o número informado: {Entity.OrderNumber}";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                await _dbContextOM.AddAsync(Entity);
                await _dbContextOM.SaveChangesAsync();

                Response.Status=ResponseStatusEnum.Success;
                Response.Message = $"Pedido criado com sucesso!\nDetalhes: {Entity.OrderNumber} | {Entity.TimeOrder}";
            }
            catch(Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message="Ocorreu um erro inesperado: "+ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }

       

        public Task<SimpleResponseModel> UpdateStatusOrder(OrderEntity Entity)
        {
            throw new NotImplementedException();
        }
    }
}
