using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.API.Controllers.QueriesControllers.Adm
{
    [ApiController]
    [Route("api/queriesOrder/admin")]
    public class OrderQueriesAdmController : Controller
    {
        private readonly ILogger<OrderQueriesAdmController> _logger;

        private readonly IOrderQueriesAdmInterface _orderQueriesAdmInterface;
        public OrderQueriesAdmController(ILogger<OrderQueriesAdmController> logger,
            IOrderQueriesAdmInterface orderQueriesAdmInterface)
        {
            _logger = logger;
            _orderQueriesAdmInterface = orderQueriesAdmInterface;
        }

        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("orders/all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var ResponseService = await _orderQueriesAdmInterface.GetAllOrders();

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
            _logger.LogInformation("Pedidos coletados com sucesso.");
            return Ok(ResponseService);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("ordersByUserEmail")]
        public async Task<IActionResult> GetAllOrdersByUserEmail([FromQuery] string email)
        {
            var ResponseService = await _orderQueriesAdmInterface.GetAllOrdersByUserEmail(new UserEmailVO(email));

            if (ResponseService.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return NotFound(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}.");
                return BadRequest(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} |  {ResponseService.Message}");
            return Ok(ResponseService);
        }


        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("order/{orderNumber}")]
        public async Task<IActionResult> GetOrderByNumber([FromRoute] int orderNumber)
        {
            var response = await _orderQueriesAdmInterface.GetOrderByNumber(new OrderNumberVO(orderNumber));

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
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} |  {response.Message}");
                return StatusCode(500, "Ocorreu um erro inesperado.");
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} |  {response.Message}");
            return Ok(response);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("orders/by-type/{typeOccurrence}")]
        public async Task<IActionResult> GetAllOrdersByTypeOccurrence([FromRoute] ETypeOccurrenceEnum typeOccurrence)
        {
            var response = await _orderQueriesAdmInterface.GetAllOrdersByTypeOccurrence(typeOccurrence);

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
