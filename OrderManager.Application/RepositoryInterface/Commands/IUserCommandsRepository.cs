using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
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
        Task<SimpleResponseModel> InactiveUserRepository(UserEmailVO email);
        Task<SimpleResponseModel> ActiveUserRepository(UserEmailVO email);
        Task<SimpleResponseModel> DeleteUserRepository(UserEmailVO email);

        Task<SimpleResponseModel> PromoteUserToAdmRepository(UserEmailVO email);
    }
}
