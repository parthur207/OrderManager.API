using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.Login
{
    public interface IUserLoginDataRepository
    {
        Task<ResponseModel<(int, RoleEnum)>?> LoginRepository(UserEntity userDataEntity);//Retorna o ID do usuario e sua 'role'
    }
}
