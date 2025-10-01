using OrderManager.Application.DTOs;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.Adm
{
    public interface IUserQueriesAdmInterface
    {
        Task<ResponseModel<List<UserDTO>>?> GetAllUsers();
        Task<ResponseModel<UserDTO>?> GetUserByEmail(UserEmailVO email);
        Task<ResponseModel<UserDTO>?> GetUserByOrderNumber(OrderNumberVO orderNumber);
    }
}
