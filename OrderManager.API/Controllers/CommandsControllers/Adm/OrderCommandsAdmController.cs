using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;

namespace OrderManager.API.Controllers.CommandsControllers.Adm
{
    [ApiController]
    [Route("api/commandsOrder/admin")]
    public class OrderCommandsAdmController : Controller
    {
        private readonly ILogger<OrderCommandsAdmController> _logger;

        public OrderCommandsAdmController(ILogger<OrderCommandsAdmController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
