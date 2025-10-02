using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Interfaces.IServices.IQueriesAdm;
using OrderManager.Domain.Enuns;

namespace OrderManager.API.Controllers.QueriesControllers.Adm
{
    [ApiController]
    [Route("api/queriesUser/admin")]
    public class UserQueriesAdmController : Controller
    {
        private readonly ILogger<UserQueriesAdmController> _logger;

        private readonly IOrderQueriesAdmInterface _orderQueriesAdmInterface;
        public UserQueriesAdmController(ILogger<UserQueriesAdmController> logger,
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
        [HttpGet("user/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok();
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet()]
        public async Task<IActionResult> GetUserByEmail()
        {
            return Ok();
        }

        [Authorize(Roles = nameof(RoleEnum.Adm))]
        [HttpGet()]
        public async Task<IActionResult> GetUserByOrderNumber()
        {
            return Ok();
        }
    }
}
