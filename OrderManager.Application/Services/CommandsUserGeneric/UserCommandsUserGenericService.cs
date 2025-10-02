using OrderManager.Application.Interfaces.IServices.ICommandsGenericUser;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Enuns;
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
        private readonly IUserCommandsRepository _userCommandsUserRepository;
        private readonly IUserMapperInterface _userMapperInterface;
        public UserCommandsUserGenericService(IUserCommandsRepository userCommandsUserRepository, IUserMapperInterface userMapperInterface)
        {
            _userCommandsUserRepository = userCommandsUserRepository;
            _userMapperInterface= userMapperInterface;
        }
        public async Task<SimpleResponseModel> CreateUser(CreateUserModel Model)
        {
            SimpleResponseModel Response = new SimpleResponseModel();    
            try
            {
                if (Model is null)
                {
                    Response.Message = "Erro. O modelo de criação de usuário não pode ser nulo.";
                    Response.Status= ResponseStatusEnum.Error;
                    return Response;
                }

                var UserEntityConverted = _userMapperInterface.UserCreateModelToEntity(Model);

                if (UserEntityConverted.Status.Equals(ResponseStatusEnum.Error) ||
                    UserEntityConverted.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= UserEntityConverted.Status;
                    Response.Message = UserEntityConverted.Message;
                    return Response;
                }

                var ResponseRespository = await _userCommandsUserRepository.CreateUserRepository(UserEntityConverted.Content);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= ResponseRespository.Status;
                    if(ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = ResponseRespository.Message;

                    return Response;
                }
                Response.Status= ResponseRespository.Status;
                Response.Message = ResponseRespository.Message;
            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: "+ex.Message;
                Response.Status= ResponseStatusEnum.CriticalError;
            }
            return Response;
        }
    }
}
