using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.API.Controllers.QueriesControllers.Adm
{
    [ApiController]
    [Route("api/queriesUser/admin")]
    public class UserQueriesAdmController : Controller
    {
        private readonly ILogger<UserQueriesAdmController> _logger;

        private readonly IUserQueriesAdmInterface _userQueriesAdmInterface;
        public UserQueriesAdmController(ILogger<UserQueriesAdmController> logger,
            IUserQueriesAdmInterface userQueriesAdmInterface)
        {
            _logger = logger;
            _userQueriesAdmInterface = userQueriesAdmInterface;
        }

        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("user/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userQueriesAdmInterface.GetAllUsers();

            if (response.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return NotFound(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return BadRequest(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
            return Ok(response);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("user/email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string Email)
        {
            var response = await _userQueriesAdmInterface.GetUserByEmail(new UserEmailVO(Email));

            if (response.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return NotFound(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return BadRequest(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
            return Ok(response);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("userByOrder/{OrderNumber}")]
        public async Task<IActionResult> GetUserByOrderNumber([FromRoute] int OrderNumber)
        {
            var response = await _userQueriesAdmInterface.GetUserByOrderNumber(new OrderNumberVO(OrderNumber));

            if (response.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return NotFound(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return BadRequest(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | {response.Message}");
            return Ok(response);
        }
    }
}
