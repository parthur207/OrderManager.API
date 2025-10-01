using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.ICommandsAdm
{
    public interface IUserCommadsAdmInterface
    {
        Task<SimpleResponseModel> InactiveUser(UserEmailVO Email);
        Task<SimpleResponseModel> ActiveUser(UserEmailVO Email);

        Task<SimpleResponseModel> DeleteUser(UserEmailVO Email);
        Task<SimpleResponseModel> PromoteUserToAdm(UserEmailVO Email);
    }
}
