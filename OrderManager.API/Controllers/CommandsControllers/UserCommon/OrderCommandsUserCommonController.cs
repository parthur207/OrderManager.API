using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.Interfaces.IServices.ICommandsUserCommon;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.ValueObjects;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderManager.API.Controllers.CommandsControllers.UserCommon
{
    [ApiController]
    [Route("api/commandsOrder/user")]
    public class OrderCommandsUserCommonController : Controller
    {
        private readonly ILogger<OrderCommandsUserCommonController> _logger;

        private readonly IOrderCommandsUserCommonInterface _orderCommandsUserCommonInterface;
        private readonly IOrderNumberGeneratorInterface _orderNumberGeneratorInterface;
        public OrderCommandsUserCommonController(ILogger<OrderCommandsUserCommonController> logger,
        IOrderCommandsUserCommonInterface orderCommandsUserCommonInterface, IOrderNumberGeneratorInterface orderNumberGeneratorInterface)
        {
            _logger = logger;
            _orderCommandsUserCommonInterface = orderCommandsUserCommonInterface;
            _orderNumberGeneratorInterface = orderNumberGeneratorInterface;
        }
        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [Authorize(Roles =nameof(RoleEnum.Common))]
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var GeneratedNumber = _orderNumberGeneratorInterface.GeneratorNumber();

            var ResponseService = await _orderCommandsUserCommonInterface.CreateOrder(GeneratedNumber, userId);

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
            _logger.LogInformation($"Ym novo pedido foi criado. Número do pedido: {GeneratedNumber} | Id do usuário: {userId}");
            return Ok(ResponseService);
        }
    }
}
