using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.API.Controllers.CommandsControllers.Adm
{
    [ApiController]
    [Route("api/commandsUser/admin")]
    public class UserCommandsAdmController : Controller
    {
        private readonly ILogger<UserCommandsAdmController> _logger;
        private readonly IUserCommadsAdmInterface _occurrenceOrderCommandsAdmInterface;
        public UserCommandsAdmController(ILogger<UserCommandsAdmController> logger,
            IUserCommadsAdmInterface occurrenceOrderCommandsAdmInterface)
        {
            _logger = logger;
            _occurrenceOrderCommandsAdmInterface = occurrenceOrderCommandsAdmInterface;
        }

        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [Authorize(Roles =nameof(RoleEnum.Adm))]
        [HttpPut("inactive/{userEmail}")]
        public async Task<IActionResult> InactiveUser([FromRoute] string userEmail)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.InactiveUser(new UserEmailVO(userEmail));

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
            _logger.LogInformation($"Usuário inativo com sucesso: {userEmail}");
            return Ok(ResponseService);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpPut("active/{userEmail}")]
        public async Task<IActionResult> ActiveUser([FromRoute] string userEmail)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.ActiveUser(new UserEmailVO(userEmail));

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
            _logger.LogInformation($"Usuário ativado com sucesso: {userEmail}");
            return Ok(ResponseService);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpPut("delete/{userEmail}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userEmail)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.DeleteUser(new UserEmailVO(userEmail));

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
            _logger.LogInformation($"Usuário deletado com sucesso: {userEmail}");
            return Ok(ResponseService);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpPut("promoteUser/{userEmail}")]
        public async Task<IActionResult> PromoteUserToAdm([FromRoute] string userEmail)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.PromoteUserToAdm(new UserEmailVO(userEmail));

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
            _logger.LogInformation($"Usuário promovido para administrador com sucesso: {userEmail}");
            return Ok(ResponseService);
        }
    }
}
