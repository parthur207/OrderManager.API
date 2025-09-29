using System;
using System.Collections.Generic;
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
        public UserEntity(string name, int? phone, string address)
        {
            Name = name;
            Phone = phone;
            Address = address;
            CreatedAt = DateTime.Now;
        }

        public int Name { get; private set; }
    }
}
