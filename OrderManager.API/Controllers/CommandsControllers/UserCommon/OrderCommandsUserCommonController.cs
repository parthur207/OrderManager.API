using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;

namespace OrderManager.API.Controllers.CommandsControllers.UserCommon
{
    [ApiController]
    [Route("api/queriesUser/user")]
    public class OrderCommandsUserCommonController : Controller
    {
        private readonly ILogger<OrderCommandsUserCommonController> _logger;

        public OrderCommandsUserCommonController(ILogger<OrderCommandsUserCommonController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
