using OrderManager.Domain.Enuns;
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
        public UserEntity(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public UserEntity(string name, string email, string password, string address)
        {
            Name = name;
            Email = email;
            Password = password;
            Addres = address;
            OrderList = [];
            Role= RoleEnum.Common;//O processo de cadastro ja atribuí o papel como comum por padrao
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        [Required]
        public string Addres { get; private set; }

        public List<OrderEntity>? OrderList { get; private set; }

        public RoleEnum Role { get; private set; }
    }
}
