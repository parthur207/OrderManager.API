using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Application.Services.CommandsAdm
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

        public async Task<SimpleResponseModel> ActiveUser(UserEmailVO Email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                if (Email is null)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O email não pode ser nulo.";
                    return Response;
                }

                var ResponseRespository = await _userCommandsRepository.ActiveUserRepository(Email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error)
                    || ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {

                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }
                Response.Message = ResponseRespository.Message;
                Response.Status = ResponseRespository.Status;

            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }

        public async Task<SimpleResponseModel> DeleteUser(UserEmailVO Email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                if (Email is null)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O email não pode ser nulo.";
                    return Response;
                }

                var ResponseRespository = await _userCommandsRepository.DeleteUserRepository(Email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error)
                    || ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }

                Response.Message = ResponseRespository.Message;
                Response.Status = ResponseRespository.Status;

            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }


        public async Task<SimpleResponseModel> InactiveUser(UserEmailVO Email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                if (Email is null)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O email não pode ser nulo.";
                    return Response;
                }

                var ResponseRespository = await _userCommandsRepository.InactiveUserRepository(Email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error)
                    || ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }

                Response.Message = ResponseRespository.Message;
                Response.Status = ResponseRespository.Status;

            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }


        public async Task<SimpleResponseModel> PromoteUserToAdm(UserEmailVO Email)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                if (Email is null)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O email não pode ser nulo.";
                    return Response;
                }

                var ResponseRespository = await _userCommandsRepository.PromoteUserToAdmRepository(Email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error)
                    || ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                        Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }

                Response.Message = ResponseRespository.Message;
                Response.Status = ResponseRespository.Status;

            }
            catch (Exception ex)
            {
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Response.Status = ResponseStatusEnum.CriticalError;
            }
            return Response;
        }
    }
}
