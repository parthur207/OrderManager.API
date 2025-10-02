using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;

namespace OrderManager.API.Controllers.CommandsControllers.Adm
{
    [ApiController]
    [Route("api/commandsOrder/admin")]
    public class OrderCommandsAdmController : Controller
    {
        private readonly ILogger<OrderCommandsAdmController> _logger;
        private readonly IOccurrenceOrderCommandsAdmInterface _occurrenceOrderCommandsAdmInterface;

        public OrderCommandsAdmController(ILogger<OrderCommandsAdmController> logger, IOccurrenceOrderCommandsAdmInterface occurrenceOrderCommandsAdmInterface)
        {
            _logger = logger;
            _occurrenceOrderCommandsAdmInterface = occurrenceOrderCommandsAdmInterface;
        }
        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpPost("newOccurrence")]
        public async Task<IActionResult> CreateOccurrenceToOrder([FromBody] CreateOccurrenceToOrderModel Model)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.CreateOccurrenceToOrder(Model);

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
            _logger.LogInformation("Uma nova ocorrência foi criada.");
            return Created();
        }
        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpPut("delteOccurence")]
        public async Task<IActionResult> DeleteOccurrenceById([FromBody] DeleteOccurrenceOrderModel Model)
        {
            var ResponseService = await _occurrenceOrderCommandsAdmInterface.DeleteOccurrenceByOrderNumber(Model);

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
            _logger.LogInformation($"Uma ocorrencia foi deletada. Id da ocorrencia: {Model.OccurrenceId} | Número do pedido: {Model.OrderNumber}");
            return Ok(ResponseService);
        }
    }
}
