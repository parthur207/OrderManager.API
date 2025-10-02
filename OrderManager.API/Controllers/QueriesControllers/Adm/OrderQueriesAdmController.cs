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
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | Nenhum pedido foi encontrado.");
                return NotFound(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | Ocorreu um erro ao buscar os pedidos.");
                return BadRequest(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | Ocorreu um erro inesperado ao buscar os pedidos.");
                return StatusCode(500, ResponseService);
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
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | Nenhum pedido foi encontrado atrelado ao email.");
                return NotFound(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | Ocorreu um erro ao buscar os pedidos atrelados ao email '{email}'.");
                return BadRequest(ResponseService);
            }
            if (ResponseService.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | Erro crítico inesperado ao buscar os pedidos atrelado ao email '{email}'");
                return StatusCode(500, ResponseService);
            }
            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | Pedidos coletados com sucesso.");
            return Ok(ResponseService);
        }


        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("order/{orderNumber}")]
        public async Task<IActionResult> GetOrderByNumber([FromRoute] int orderNumber)
        {
            var response = await _orderQueriesAdmInterface.GetOrderByNumber(new OrderNumberVO(orderNumber));

            if (response.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | Pedido '{orderNumber}' não foi encontrado.");
                return NotFound(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | Erro ao buscar pedido '{orderNumber}'.");
                return BadRequest(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | Erro crítico inesperado ao buscar pedido '{orderNumber}'.");
                return StatusCode(500, response);
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | Pedido '{orderNumber}' coletado com sucesso.");
            return Ok(response);
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet("orders/by-type/{typeOccurrence}")]
        public async Task<IActionResult> GetAllOrdersByTypeOccurrence([FromRoute] ETypeOccurrenceEnum typeOccurrence)
        {
            var response = await _orderQueriesAdmInterface.GetAllOrdersByTypeOccurrence(typeOccurrence);

            if (response.Status.Equals(ResponseStatusEnum.NotFound))
            {
                _logger.LogWarning($"EndPoint: {GetCurrentEndpoint()} | Nenhum pedido encontrado para o tipo {typeOccurrence}.");
                return NotFound(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.Error))
            {
                _logger.LogError($"EndPoint: {GetCurrentEndpoint()} | Erro ao buscar pedidos do tipo {typeOccurrence}.");
                return BadRequest(response);
            }
            if (response.Status.Equals(ResponseStatusEnum.CriticalError))
            {
                _logger.LogCritical($"EndPoint: {GetCurrentEndpoint()} | Erro crítico inesperado ao buscar pedidos do tipo '{typeOccurrence}'.");
                return StatusCode(500, response);
            }

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | Pedidos do tipo '{typeOccurrence}' coletados com sucesso.");
            return Ok(response);
        }

    }
}
