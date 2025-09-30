using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
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


    }
}
