using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers.AdminControllers
{
    public class UserControllerAdm : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
