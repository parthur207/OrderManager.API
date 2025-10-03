using Microsoft.EntityFrameworkCore;
using OrderManager.Domain.Entities;
using OrderManager.Domain.ValueObjects;

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
            
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.OrderList)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.Occurrences)
                .WithOne(oc => oc.Order)
                .HasForeignKey(oc => oc.OrderNumber)
                .HasPrincipalKey(o => o.OrderNumber)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region FluentAPI - UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.Email)
                      .HasConversion(
                          v => v.Value,
                          v => new UserEmailVO(v))
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.Password)
                      .HasConversion(
                          v => v.Value,
                          v => new PasswordVO(v))
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(u => u.Address)
                      .HasMaxLength(250);
            });
            #endregion

            #region FluentAPI - OrderEntity
            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(o => o.OrderNumber)
                      .HasConversion(
                          v => v.Value,
                          v => new OrderNumberVO(v))
                      .HasColumnName("OrderNumber")
                      .IsRequired();

                entity.HasAlternateKey(o => o.OrderNumber); 

                entity.Property(o => o.TimeOrder)
                      .IsRequired();
            });
            #endregion

            #region FluentAPI - OccurrenceEntity
            modelBuilder.Entity<OccurrenceEntity>(entity =>
            {
                entity.HasKey(oc => oc.Id);

                entity.Property(oc => oc.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(oc => oc.OrderNumber)
                      .HasConversion(
                          v => v.Value,
                          v => new OrderNumberVO(v))
                      .IsRequired();
            });
            #endregion
        }

    }
}
