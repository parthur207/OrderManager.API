using Microsoft.EntityFrameworkCore;
using OrderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Persistence
{
    public class DbContextOrderManager : DbContext
    {
        public DbContextOrderManager(DbContextOptions<DbContextOrderManager> options) : base(options)
        {
        }

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<OrderEntity> OrderEntity { get; set; }
        public DbSet<OcccurrenceEntity> OcccurrenceEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

    }
}
