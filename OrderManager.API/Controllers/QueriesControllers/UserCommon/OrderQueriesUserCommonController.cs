using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Controllers.QueriesControllers.Adm;

namespace OrderManager.API.Controllers.QueriesControllers.UserCommon
{
    [ApiController]
    [Route("api/queriesOrder/user")]
    public class OrderQueriesUserCommonController : Controller
    {
        private readonly ILogger<OrderQueriesUserCommonController> _logger;
        public OrderQueriesUserCommonController(ILogger<OrderQueriesUserCommonController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
