using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers.UserCommom
{
    public class OrderControllerUserCommon : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
