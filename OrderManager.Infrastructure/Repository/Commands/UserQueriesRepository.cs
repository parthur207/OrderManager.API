using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.Queries;
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
    public class UserQueriesRepository : IUserQueriesRepository
    {

        private readonly DbContextOrderManager _dbContextOM;

        public UserQueriesRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }

        public async Task<ResponseModel<List<UserEntity>>?> GetAllUsersRepository()
        {
            ResponseModel<List<UserEntity>?> Response = new ResponseModel<List<UserEntity>?>();

            try
            {
                var allUsers = await _dbContextOM.UserEntity
                    .Include(x => x.OrderList)
                    .ToListAsync();

                if (allUsers is null || !allUsers.Any())
                {
                    Response.Message = $"Nenhum usuário foi encontrado.";
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }

                Response.Content = allUsers;
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

        public async Task<ResponseModel<UserEntity>?> GetUserByEmailRepository(string email)
        {
            ResponseModel<UserEntity?> Response = new ResponseModel<UserEntity?>();

            try
            {
                var useByEmail = await _dbContextOM.UserEntity
                    .Include(x=>x.OrderList)
                    .FirstOrDefaultAsync(x=>x.Email==email);

                if (useByEmail is null)
                {
                    Response.Message = $"Nenhum usuário encontrado com o email informado: {email}";
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }

                Response.Content = useByEmail;
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
    

        public async Task<ResponseModel<UserEntity>?> GetUserByOrderNumberRepository(int orderNumber)
        {
            ResponseModel<UserEntity?> Response= new ResponseModel<UserEntity?>();
            
            try
            {
                var useByOrderNumber= await _dbContextOM.UserEntity
                    .Include(x=>x.OrderList)
                    .Where(x=>x.OrderList.Any(o=>o.OrderNumber==orderNumber))
                    .FirstOrDefaultAsync();

                if (useByOrderNumber is null)
                {
                    Response.Message = $"Nenhum usuário encontrado para o número do pedido informado: {orderNumber}";
                    Response.Status= ResponseStatusEnum.NotFound;
                    return Response;
                }

                Response.Content= useByOrderNumber;
                Response.Status= ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message= "Ocorreu um erro inesperado: " + ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }
    }
}
