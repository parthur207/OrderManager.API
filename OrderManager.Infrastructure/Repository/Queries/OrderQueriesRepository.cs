using Azure;
using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.Queries
{
    public class OrderQueriesRepository : IOrderQueriesRepository
    {
        private readonly DbContextOrderManager _dbContextOM;

        public OrderQueriesRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }

        public async Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersRepository()
        {
            ResponseModel<List<OrderEntity>?> Response = new ResponseModel<List<OrderEntity>?>();
            try
            {
               var AllOrders= await _dbContextOM.OrderEntity
                    .Include(x=>x.Occurrences)
                    .Include(x=>x.User)
                    .ToListAsync();

                if (AllOrders is null || !AllOrders.Any())
                {
                    Response.Message = "Nenhum pedido foi encontrado.";
                    Response.Status= ResponseStatusEnum.NotFound;
                    return Response;
                }
                Response.Content= AllOrders;
                Response.Status= ResponseStatusEnum.Success;
            }
            catch(Exception ex)
            {
                Response.Message="Ocorreu um erro inesperado: " + ex.Message;
                Response.Status=ResponseStatusEnum.CriticalError;
            }
            return Response;
        }

        public async Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByTypeOccurrenceRepository(ETypeOccurrenceEnum occurrenceEnum)
        {
            ResponseModel<List<OrderEntity>?> Response = new ResponseModel<List<OrderEntity>?>();
            try
            {
                var AllOrdersByTypeOccurrence = await _dbContextOM.OrderEntity
                     .Include(x => x.Occurrences)
                     .Include(x => x.User)
                     .Where(x => x.Occurrences.Any(o => o.TypeOccurrence == occurrenceEnum))
                     .ToListAsync();

                if (AllOrdersByTypeOccurrence is null || !AllOrdersByTypeOccurrence.Any())
                {
                    Response.Message = $"Nenhum pedido com o tipo de ocorrência '{occurrenceEnum}' foi encontrado.";
                    Response.Status= ResponseStatusEnum.NotFound;
                    return Response;
                }
                Response.Content=AllOrdersByTypeOccurrence;
                Response.Status= ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }

        public async Task<ResponseModel<OrderEntity>?> GetOrderByNumberRepository(OrderNumberVO OrderNumber)
        {
            ResponseModel<OrderEntity>? Response = new ResponseModel<OrderEntity>();
            try
            {
                var OrderById = await _dbContextOM.OrderEntity
                     .Include(x => x.Occurrences)
                     .Include(x => x.User)
                     .FirstOrDefaultAsync(x => x.OrderNumber.Value == OrderNumber.Value);

                if (OrderById is null)
                {
                    Response.Status=ResponseStatusEnum.NotFound;
                    Response.Message = $"Nenhum pedido foi encontrado com o número informado: {OrderNumber.Value}";
                    return Response;
                }

                Response.Content = OrderById;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Message="Ocorreu um erro inesperado: " + ex.Message;
                Response.Status=ResponseStatusEnum.CriticalError;
            }
            return Response;
        }

        public async Task<ResponseModel<List<OrderEntity>?>> GetAllOrdersByUserEmailRepository(UserEmailVO email)//para usuario comum e adm
        {
            ResponseModel<List<OrderEntity>?> Response = new ResponseModel<List<OrderEntity>?>();
            try
            {
                var AllOrdersByUserEmail = await _dbContextOM.OrderEntity
                     .Include(x => x.Occurrences)
                     .Include(x => x.User)
                     .Where(x=>x.User.Email.Value== email.Value)
                     .ToListAsync();

                if (AllOrdersByUserEmail is null || !AllOrdersByUserEmail.Any())
                {
                    Response.Message = $"Nenhum pedido foi efetuado até então pelo usuário.";
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }
                Response.Content = AllOrdersByUserEmail;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }

        public async Task<ResponseModel<List<OrderEntity>>?> GetAllOrdersByUserIdRepository(int userId)
        {
            ResponseModel<List<OrderEntity>?> Response = new ResponseModel<List<OrderEntity>?>();
            try
            {
                var AllOrdersByUserId = await _dbContextOM.OrderEntity
                     .Include(x => x.Occurrences)
                     .Include(x => x.User)
                     .Where(x => x.User.Id == userId)
                     .ToListAsync();

                if (AllOrdersByUserId is null || !AllOrdersByUserId.Any())
                {
                    Response.Message = $"Nenhum pedido foi encontrado.";
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }
                Response.Content = AllOrdersByUserId;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }
    }
}
