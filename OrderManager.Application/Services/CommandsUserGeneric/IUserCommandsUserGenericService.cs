using OrderManager.Application.Interfaces.IServices.ICommandsGenericUser;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.CommandsUserGeneric
{
    public class UserCommandsUserGenericService : IUserCommandsUserGenericInterface
    {
        private readonly IUserCommandsUserGenericInterface _userCommandsUserGenericInterface;
        private readonly IUserMapperInterface _userMapperInterface;
        public UserCommandsUserGenericService(IUserCommandsUserGenericInterface userCommandsUserGenericInterface, IUserMapperInterface userMapperInterface)
        {
            _userCommandsUserGenericInterface = userCommandsUserGenericInterface;
            _userMapperInterface= userMapperInterface;
        }
        public async Task<SimpleResponseModel> CreateUser(CreateUserModel Model)
        {
            throw new NotImplementedException();
        }
    }
}
