using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;

namespace OrderManager.API.Controllers.CommandsControllers.Adm
{
    [ApiController]
    [Route("api/commandsUser/admin")]
    public class UserCommandsAdmController : Controller
    {
        private readonly ILogger<UserCommandsAdmController> _logger;

        public UserCommandsAdmController(ILogger<UserCommandsAdmController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
