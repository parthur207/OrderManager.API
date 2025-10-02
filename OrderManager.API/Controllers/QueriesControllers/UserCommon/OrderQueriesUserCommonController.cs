using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;
using OrderManager.Application.Interfaces.IServices.IQueriesUserCommon;
using OrderManager.Domain.Enuns;
using System.Security.Claims;

namespace OrderManager.API.Controllers.QueriesControllers.UserCommon
{
    [ApiController]
    [Route("api/queriesOrder/user")]
    public class OrderQueriesUserCommonController : Controller
    {
        private readonly ILogger<OrderQueriesUserCommonController> _logger;
        private readonly IOrderQueriesUserCommonInterface _orderQueriesUserCommonInterface;
        public OrderQueriesUserCommonController(ILogger<OrderQueriesUserCommonController> logger, 
            IOrderQueriesUserCommonInterface orderQueriesUserCommonInterface)
        {
            _logger = logger;
            _orderQueriesUserCommonInterface = orderQueriesUserCommonInterface;
        }
        private string GetCurrentEndpoint()
        {
            return HttpContext.GetEndpoint()?.DisplayName ?? HttpContext.Request.Path;
        }


        [Authorize(Roles =nameof(RoleEnum.Common))]
        [HttpGet()]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var ResponseService = await _orderQueriesUserCommonInterface.GetOrdersByUserId(userId);
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

            _logger.LogInformation($"EndPoint: {GetCurrentEndpoint()} | {ResponseService.Message}");
            return Ok(ResponseService);
        }
    }
}
