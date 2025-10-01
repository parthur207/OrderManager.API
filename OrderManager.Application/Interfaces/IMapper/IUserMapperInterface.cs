using OrderManager.Application.DTOs.Adm;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers.MappersInterface
{
    public interface IUserMapperInterface
    {
        //Entity=> DTO
        ResponseModel<UserAdmDTO>? UserEntityToDTO(UserEntity entity);

        //Model => Entity
        ResponseModel<UserEntity>? UserCreateModelToEntity(CreateUserModel UserModel);//Para cadastros
        ResponseModel<UserEntity>? UserLoginModelToEntity(UserLoginModel UserModel);//Para updates

    }
}
