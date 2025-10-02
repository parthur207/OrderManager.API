using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers.CommandsControllers
{
    public class OrderCommandsUserCommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
