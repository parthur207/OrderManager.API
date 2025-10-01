using OrderManager.Application.DTOs.Adm;
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
        public Task<ResponseModel<List<UserAdmDTO>>?> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserAdmDTO>?> GetUserByEmail(UserEmailVO email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserAdmDTO>?> GetUserByOrderNumber(OrderNumberVO orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
