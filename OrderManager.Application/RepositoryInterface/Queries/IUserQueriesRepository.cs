using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Queries
{
    public interface IUserQueriesRepository
    {
        Task<ResponseModel<List<UserEntity>>?> GetAllUsersRepository();
        Task<ResponseModel<UserEntity>?> GetUserByEmailRepository(UserEmailVO email);
        Task<ResponseModel<UserEntity>?> GetUserByOrderNumberRepository(OrderNumberVO orderNumber);
    }
}
