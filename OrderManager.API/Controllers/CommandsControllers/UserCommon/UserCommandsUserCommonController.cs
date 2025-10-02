using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Domain.Models;
using OrderManager.Infrastructure.Auth.JWT;

namespace OrderManager.API.Controllers.CommandsControllers.UserCommon
{
    [ApiController]
    [Route("api/commandsUser/user")]
    public class UserCommandsUserCommonController : Controller
    {
        private readonly ILogger<UserCommandsUserCommonController> _logger;
        private readonly IJwtInterface _jwtInterface;
        private readonly IUserCommandsUserCommonInterface _userCommandsUserCommonInterface;
        public UserCommandsUserCommonController(ILogger<UserCommandsUserCommonController> logger, 
            IJwtInterface jwtInterface, IUserCommandsUserCommonInterface userCommandsUserCommonInterface)
        {
            _logger = logger;
            _jwtInterface = jwtInterface;
            _userCommandsUserCommonInterface = userCommandsUserCommonInterface;
        }
        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [AllowAnonymous]
        [HttpPost("create/user")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel userModel)
        {
            return Created();
        }
        [AllowAnonymous]
        [HttpPost("login/user")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginUserModel)
        {
            return Ok();
        }
        
    }
}
