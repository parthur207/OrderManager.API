using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Application.Services.Commands
{
    public class UserCommandsAdmService : IUserCommadsAdmInterface
    {
        private readonly IUserCommandsRepository _userCommandsRepository;
        private readonly IUserMapperInterface _userMapper;

        public UserCommandsAdmService(IUserCommandsRepository userCommandsRepository, IUserMapperInterface userMapper)
        {
            _userCommandsRepository = userCommandsRepository;
            _userMapper = userMapper;
        }

        public Task<SimpleResponseModel> ActiveUser(UserEmailVO Email)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleResponseModel> DeleteUser(UserEmailVO Email)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleResponseModel> InactiveUser(UserEmailVO Email)
        {
            throw new NotImplementedException();
        }

        public Task<SimpleResponseModel> PromoteUserToAdm(UserEmailVO Email)
        {
            throw new NotImplementedException();
        }
    }
}
