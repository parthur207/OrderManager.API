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
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        public string? Addres { get; private set; }

        public List<OrderEntity>? OrderList { get; private set; }

        public 


    }
}
