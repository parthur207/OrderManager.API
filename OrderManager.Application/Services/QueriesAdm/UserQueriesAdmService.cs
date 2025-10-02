using OrderManager.Application.DTOs.Adm;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Application.RepositoryInterface.Queries;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.QueriesAdm
{
    public class UserQueriesAdmService : IUserQueriesAdmInterface
    {
        private readonly IUserQueriesRepository _userQueriesRepository;
        private readonly IUserMapperInterface _userMapperInterface;
        public UserQueriesAdmService(IUserQueriesRepository userQueriesRepository, IUserMapperInterface userMapperInterface)
        {
            _userQueriesRepository = userQueriesRepository;
            _userMapperInterface = userMapperInterface;
        }
        public async Task<ResponseModel<List<UserAdmDTO>>?> GetAllUsers()
        {
            ResponseModel<List<UserAdmDTO>>? Response = new ResponseModel<List<UserAdmDTO>>();
            try
            {
                var ResponseRespository = await _userQueriesRepository.GetAllUsersRepository();

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }
                var UsersDTO= _userMapperInterface.UserEntityListToDTOList(ResponseRespository.Content);

                Response.Content = UsersDTO.Content;
                Response.Status = UsersDTO.Status;
                Response.Message = UsersDTO.Message;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }

        public async Task<ResponseModel<UserAdmDTO>?> GetUserByEmail(UserEmailVO email)
        {
            ResponseModel<UserAdmDTO>? Response = new ResponseModel<UserAdmDTO>();
            try
            {
                var ResponseRespository = await _userQueriesRepository.GetUserByEmailRepository(email);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }

                var UserDTO = _userMapperInterface.UserEntityToDTO(ResponseRespository.Content);

                Response.Content = UserDTO.Content;
                Response.Status = UserDTO.Status;
                Response.Message = UserDTO.Message;

            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message= "Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }

        public async Task<ResponseModel<UserAdmDTO>?> GetUserByOrderNumber(OrderNumberVO orderNumber)
        {
            ResponseModel<UserAdmDTO>? Response = new ResponseModel<UserAdmDTO>();
            try
            {
                var User= await _userQueriesRepository.GetUserByOrderNumberRepository(orderNumber);
                
                if (User.Status.Equals(ResponseStatusEnum.Error) ||
                   User.Status.Equals(ResponseStatusEnum.CriticalError) ||
                   User.Status.Equals(ResponseStatusEnum.NotFound))
                {
                    Response.Status = User.Status;
                    Response.Message = User.Message;
                    return Response;
                }
                var UserDTO = _userMapperInterface.UserEntityToDTO(User.Content);
                Response.Content = UserDTO.Content;
                Response.Status = UserDTO.Status;
                Response.Message = UserDTO.Message;
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
