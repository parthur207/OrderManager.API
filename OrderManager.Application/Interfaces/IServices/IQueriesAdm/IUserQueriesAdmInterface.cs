using OrderManager.Application.DTOs.Adm;
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
        Task<ResponseModel<List<UserAdmDTO>>?> GetAllUsers();
        Task<ResponseModel<UserAdmDTO>?> GetUserByEmail(UserEmailVO email);
        Task<ResponseModel<UserAdmDTO>?> GetUserByOrderNumber(OrderNumberVO orderNumber);
    }
}
