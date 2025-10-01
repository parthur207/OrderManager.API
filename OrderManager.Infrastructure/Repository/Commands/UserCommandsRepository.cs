using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.Commands
{
    public class UserCommandsRepository : IUserCommandsRepository
    {

        private readonly DbContextOrderManager _dbContextOM;

        public UserCommandsRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }

        public async Task<SimpleResponseModel> ActiveUserRepository(string email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                var userEntity = await _dbContextOM.UserEntity.FirstOrDefaultAsync(x => x.Email.Value == email);
                if (userEntity is null)
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = "Nenhum usuário com o email informado foi encontrado.";
                    Debug.Assert(false, Response.Message);
                    return Response;
                }
                userEntity.SetToActive();
                _dbContextOM.UserEntity.Update(userEntity);
                await _dbContextOM.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
            }
            return Response;
        }

        public async Task<SimpleResponseModel> CreateUserRepository(UserEntity Entity)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                if (await _dbContextOM.UserEntity.AnyAsync(x => x.Email == Entity.Email))
                {
                    Response.Message = "Erro. Usuário já cadastrado.";
                    Response.Status = ResponseStatusEnum.Error;
                    Debug.Assert(false, Response.Message);
                    return Response;
                }

                await _dbContextOM.AddAsync(Entity); ;
                await _dbContextOM.SaveChangesAsync();

                Response.Status = ResponseStatusEnum.Success;
                Response.Message = "Cadastro realizado com sucesso!";
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Debug.Assert(false, Response.Message);
            }

            return Response;
        }

        public async Task<SimpleResponseModel> DeleteUserRepository(string email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                var userEntity = await _dbContextOM.UserEntity.FirstOrDefaultAsync(x => x.Email.Value == email);
                if (userEntity is null)
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = "Nenhum usuário com o email informado foi encontrado.";
                    Debug.Assert(false, Response.Message);
                    return Response;
                }

                _dbContextOM.UserEntity.Remove(userEntity);
                await _dbContextOM.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Debug.Assert(false, Response.Message);

            }
            return Response;
        }

        public async Task<SimpleResponseModel> InactiveUserRepository(string email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                var userEntity = await _dbContextOM.UserEntity.FirstOrDefaultAsync(x => x.Email.Value == email);
                if (userEntity is null)
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = "Nenhum usuário com o email informado foi encontrado.";
                    Debug.Assert(false, Response.Message);
                    return Response;
                }

                var response = userEntity.SetToInactive();
                if (response)
                {
                    _dbContextOM.UserEntity.Update(userEntity);
                    await _dbContextOM.SaveChangesAsync();
                    Response.Status = ResponseStatusEnum.Success;
                }
                else
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O usuário já se inativo.";
                }
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
