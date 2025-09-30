using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Commands
{
    public interface IUserCommandsRepository
    {
        Task<SimpleResponseModel> CreateUserRepository(UserEntity Entity);
        Task<SimpleResponseModel> InactiveUserRepository(string email);
        Task<SimpleResponseModel> ActiveUserRepository(string email);
        Task<SimpleResponseModel> DeleteUserRepository(string email);
    }
}
