using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;
using System;
using Xunit;

namespace OrderManager.Tests.Domain.Entities
{
    public class UserEntityTests
    {
        [Fact]
        public void Ctor_WithEmailAndPassword_SetsPropertiesCorrectly()
        {
            var email = new UserEmailVO("user@test.com");
            var password = new PasswordVO("Pass123");

            var user = new UserEntity(email, password);

            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Null(user.Name);
            Assert.Null(user.Address);
            Assert.Null(user.OrderList);
        }

        [Fact]
        public void Ctor_WithNameEmailPasswordAddress_SetsPropertiesCorrectly()
        {
            string name = "Paulo";
            var email = new UserEmailVO("paulo@test.com");
            var password = new PasswordVO("Pass123");
            string address = "Rua Exemplo, 123";

            var user = new UserEntity(name, email, password, address);

            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(address, user.Address);
            Assert.NotNull(user.OrderList);
            Assert.Empty(user.OrderList);
            Assert.Equal(RoleEnum.Common, user.Role);
            Assert.True((DateTime.Now - user.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void PromoteToAdmin_ChangesRoleToAdm()
        {
            var user = new UserEntity("Paulo", new UserEmailVO("paulo@test.com"), new PasswordVO("Pass123"), "Rua Exemplo, 123");

            user.PromoteToAdmin();

            Assert.Equal(RoleEnum.Adm, user.Role);
        }
    }
}
