using OrderManager.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.DTOs
{
        public class UserDataTokenDTO
        {
            public int Id { get; set; }
            public RoleEnum Role { get; set; }
        }
    }
}
