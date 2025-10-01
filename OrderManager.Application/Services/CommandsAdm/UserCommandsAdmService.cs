using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Mappers.MappersInterface;
using OrderManager.Application.RepositoryInterface.Commands;
using IUserMapperInterface = OrderManager.Application.Interfaces.IMapper.IUserMapperInterface;

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
    }
}
