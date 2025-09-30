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
        public DbSet<OccurrenceEntity> OccurrenceEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Relacionamento entre as entidades
            //1:n | User:Order
            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.OrderList)
                .WithOne(x => x.Requester)
                .HasForeignKey(x => x.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            //1:n | Order:Occurrences
            modelBuilder.Entity<OrderEntity>()
                .HasMany(x => x.Occurrences)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region FluentAPI
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(200);
                entity.Property(u => u.Addres).HasMaxLength(250);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderNumber).IsRequired();
            });

            modelBuilder.Entity<OccurrenceEntity>(entity =>
            {
                entity.HasKey(oc => oc.Id);
                entity.Property(oc => oc.TypeOccurrence).IsRequired();
            });
            #endregion
        }
    }

    }
}
