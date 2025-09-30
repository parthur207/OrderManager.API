using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
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

        public Task<ResponseModel<List<UserEntity>>?> GetAllUsersRepository()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<UserEntity>>?> GetUserByEmailRepository()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<UserEntity>>?> GetUserByOrderNumberRepository()
        {
            throw new NotImplementedException();
        }
    }
}
