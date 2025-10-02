using OrderManager.Application.DTOs.Adm;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Mappers
{
    public class UserMapper : IUserMapperInterface
    {
        private readonly IOrderMapperInterface _orderMapperInterface;
        public UserMapper(IOrderMapperInterface orderMapperInterface)
        {
            _orderMapperInterface = orderMapperInterface;
        }

        public ResponseModel<List<UserAdmDTO>?> UserEntityListToDTOList(List<UserEntity> entityList)
        {
            ResponseModel<List<UserAdmDTO>?> Response = new ResponseModel<List<UserAdmDTO>?>();
            try
            {
                if (entityList is null || !entityList.Any())
                {
                    Response.Message = "Erro. A lista de entidades de usuário não pode ser nula ou vazia.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var userDTOListConverted = new List<UserAdmDTO>();

                foreach (var entity in entityList)
                {
                    var userDTOConverted = new UserAdmDTO
                    (
                        entity.Name,
                        entity.Email.Value,
                        entity.Address,
                        _orderMapperInterface.OrderEntityListToDTOList(entity.OrderList).Content,
                        entity.CreatedAt,
                        entity.Role
                    );
                    userDTOListConverted.Add(userDTOConverted);
                }

                Response.Content = userDTOListConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception EX)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = EX.Message;
            }
            return Response;
        }

        public ResponseModel<UserEntity>? UserCreateModelToEntity(CreateUserModel UserModel)
        {
            ResponseModel<UserEntity>? Response= new ResponseModel<UserEntity>();
            try
            {
                if (UserModel is null)
                {
                    Response.Message ="Erro. O modelo de usuário não pode ser nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var UserEntityConverted = new UserEntity
                (
                    UserModel.Name,
                    new UserEmailVO(UserModel.Email),
                    new PasswordVO(UserModel.Password),
                    UserModel.Address
                );

                Response.Content = UserEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception EX)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = EX.Message;
            }
            return Response;
        }

        public ResponseModel<UserEntity>? UserLoginModelToEntity(UserLoginModel UserModel)
        {
            ResponseModel<UserEntity>? Response = new ResponseModel<UserEntity>();
            try
            {
                if (UserModel is null)
                {
                    Response.Message = "Erro. O modelo de usuário para login não pode ser nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var UserEntityConverted = new UserEntity
                (
                    new UserEmailVO(UserModel.Email),
                    new PasswordVO(UserModel.Password)
                );

                Response.Content = UserEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception EX)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = EX.Message;
            }
            return Response;
        }

        //entity=>dto
        public ResponseModel<UserAdmDTO>? UserEntityToDTO(UserEntity entity)
        {
            ResponseModel<UserAdmDTO>? Response = new ResponseModel<UserAdmDTO>();
            try
            {
                if (entity is null)
                {
                    Response.Message = "Erro. A entidade de usuário não pode ser nula.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var userDTOConverted = new UserAdmDTO
                (
                    entity.Name,
                    entity.Email.Value,
                    entity.Address,
                    _orderMapperInterface.OrderEntityListToDTOList(entity.OrderList).Content,
                    entity.CreatedAt,
                    entity.Role
                );

                Response.Content = userDTOConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception EX)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = EX.Message;
            }
            return Response;
        }
    }
}
