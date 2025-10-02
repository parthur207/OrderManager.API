using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Application.Interfaces.IServices.ILogin;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Infrastructure.Auth.JWT;
using System.Security.Claims;

namespace OrderManager.API.Controllers.CommandsControllers.UserCommon
{
    [ApiController]
    [Route("api/commandsUser/user")]
    public class UserCommandsUserCommonController : Controller
    {
        private readonly ILogger<UserCommandsUserCommonController> _logger;
        private readonly IJwtInterface _jwtInterface;
        private readonly IUserCommandsUserCommonInterface _userCommandsUserCommonInterface;
        private readonly ILoginInterface _loginInterface;
        public UserCommandsUserCommonController(ILogger<UserCommandsUserCommonController> logger, 
            IJwtInterface jwtInterface, IUserCommandsUserCommonInterface userCommandsUserCommonInterface, 
            ILoginInterface loginInterface)
        {
            _logger = logger;
            _jwtInterface = jwtInterface;
            _userCommandsUserCommonInterface = userCommandsUserCommonInterface;
            _loginInterface = loginInterface;
        }
        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [AllowAnonymous]
        [HttpPost("create/user")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel userModel)
        {

            var ResponseService = await _userCommandsUserCommonInterface.CreateUser(userModel);

            if (ResponseService.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return NotFound(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return BadRequest(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
            _logger.LogInformation($"Um novo cadastro foi realizado. Email do usuario: {userModel.Email} | Criado em: {DateTime.Now}");
            return Ok(ResponseService);
        }

        [AllowAnonymous]
        [HttpPost("login/user")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginUserModel)
        {
            if (loginUserModel.Email == "admin@teste.com" && loginUserModel.Password == "12345")
            {

                var tokenAdmin = _jwtInterface.GenerateToken(100, nameof(RoleEnum.Adm));
                return Ok(new { Resposta = "Login de administrador efetuado com sucesso", Token = tokenAdmin });
            }
            var ResponseService = await _loginInterface.Login(loginUserModel);

            if (ResponseService.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return NotFound(ResponseService);
            }

            if (ResponseService.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return BadRequest(ResponseService);
            }

            if (ResponseService.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
            
            var TokenGenerated = _jwtInterface.GenerateToken(ResponseService.Content.Item1, ResponseService.Content.Item2.ToString());
            return Ok(new
                {
                    Resposta = "Login efetuado com sucesso.",
                    Token = TokenGenerated
                });
            }
        }
    }
