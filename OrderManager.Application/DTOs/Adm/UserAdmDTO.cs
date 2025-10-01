using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTOs.Adm
{
    public class UserAdmDTO
    {
        public UserAdmDTO(string name, string email, string address, List<OrderDTO> orderList, DateTime createdAt, RoleEnum role)
        {
            Name = name;
            Email = email;
            Address = address;
            OrderList = orderList;
            CreatedAt = createdAt;
            Role = role;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<OrderDTO> OrderList { get; set; }
        public DateTime CreatedAt { get; set; }
        public RoleEnum Role { get; set; }
    }
}
