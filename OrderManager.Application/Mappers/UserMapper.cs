using OrderManager.Application.DTOs;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers
{
    public class UserMapper : IUserMapperInterface
    {
        public ResponseModel<UserEntity>? UserCreateModelToEntity(CreateUserModel UserModel)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<UserDTO>? UserEntityToDTO(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<UserEntity>? UserLoginModelToEntity(UserLoginModel UserModel)
        {
            throw new NotImplementedException();
        }
    }
}
