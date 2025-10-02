using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.ILogin;
using OrderManager.Application.RepositoryInterface.Login;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.Login
{
    public class LoginService : ILoginInterface
    {
        private readonly IUserLoginDataRepository _userLoginDataRepository;
        private readonly IUserMapperInterface _userMapperInterface;
        public LoginService(IUserLoginDataRepository userLoginDataRepository, IUserMapperInterface userMapperInterface)
        {
            _userLoginDataRepository = userLoginDataRepository;
            _userMapperInterface = userMapperInterface;
        }
        public async Task<ResponseModel<(int, RoleEnum)>> Login(UserLoginModel Model)
        {
            ResponseModel<(int, RoleEnum)> Response = new ResponseModel<(int, RoleEnum)>();
            try
            {
                if (Model is null)
                {
                    Response.Message = "Erro. O modelo de login não pode ser nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var UserEntityConverted = _userMapperInterface.UserLoginModelToEntity(Model);

                if (UserEntityConverted.Status.Equals(ResponseStatusEnum.Error) ||
                    UserEntityConverted.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = UserEntityConverted.Status;
                    Response.Message = UserEntityConverted.Message;
                    return Response;
                }

                var ResponseRespository = await _userLoginDataRepository.LoginRepository(UserEntityConverted.Content);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                   ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;    
                }
                Response.Status = ResponseRespository.Status;
                Response.Message = ResponseRespository.Message;
                Response.Content = ResponseRespository.Content;
                return Response;
            }
            catch (Exception ex)
            {
                //Log Exception
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado.";
                return Response;
            }
        }
    }
}

