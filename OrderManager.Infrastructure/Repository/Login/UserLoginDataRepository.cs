using OrderManager.Application.RepositoryInterface.Login;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.Login
{
    public class UserLoginDataRepository : IUserLoginDataRepository
    {
        private readonly DbContextOrderManager _dbContextOM;

        public UserLoginDataRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }
        public async Task<ResponseModel<(int, RoleEnum)>?> LoginRepository(UserEntity userDatalogin)
        {
            ResponseModel<(int, RoleEnum)> Response = new ResponseModel<(int, RoleEnum)>();

            try
            {
                var UserExists = await _dbContextOM.UserEntity
                     .Where(x => x.IsActive == true)
                     .FirstOrDefaultAsync(x => x.Email.Value == userDatalogin.Email.Value && x.Password.Value==userDatalogin.Password.Value);

                if (UserExists is null)
                {
                    Response.Message = "Email ou senha incorretos.";
                    Response.Status=ResponseStatusEnum.NotFound;
                    return Response;
                }

                Response.Status=ResponseStatusEnum.Success;
                Response.Content=(UserExists.Id, UserExists.Role);
            }
            catch (Exception ex)
            {
                Response.Status=ResponseStatusEnum.CriticalError;
                Response.Message="Ocorreu um erro inesperado: " + ex.Message;
            }
            return Response;
        }
    }
}
