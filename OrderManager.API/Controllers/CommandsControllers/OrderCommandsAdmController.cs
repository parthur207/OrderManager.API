using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers.CommandsControllers
{
    public class OrderCommandsAdmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
