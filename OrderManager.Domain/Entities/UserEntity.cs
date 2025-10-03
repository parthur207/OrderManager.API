using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManager.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        //para logins
        public UserEntity(UserEmailVO email, PasswordVO password)
        {
            Email = email;
            Password = password;
        }

        //para cadastros
        public UserEntity(string name, UserEmailVO email, PasswordVO password, string address)
        {
            Name = name;
            Email = email;
            Password = password;
            Address = address;
            OrderList = new List<OrderEntity>();
            CreatedAt=DateTime.Now;
            Role = RoleEnum.Common;//O processo de cadastro ja atribuí o papel como 'comum' por padrao
        }

        public string Name { get; private set; }

        public UserEmailVO Email { get; private set; }

        public PasswordVO Password { get; private set; }

        public string Address { get; private set; }

        public List<OrderEntity>? OrderList { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public RoleEnum Role { get; private set; }

        public void PromoteToAdmin() => Role = RoleEnum.Adm;

    }
}
