using OrderManager.Application.Interfaces.IServices.ILoginInterface;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.Login
{
    public class LoginService : ILoginInterface
    {
        public Task<ResponseModel<(int, RoleEnum)>> Login(UserLoginModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
