using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IServices.Adm;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.QueriesAdm
{
    public class UserQueriesAdmService : IUserQueriesAdmInterface
    {
        public Task<ResponseModel<List<UserDTO>>?> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserDTO>?> GetUserByEmail(UserEmailVO email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserDTO>?> GetUserByOrderNumber(OrderNumberVO orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
