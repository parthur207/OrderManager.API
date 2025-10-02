using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers.QueriesControllers.UserCommon
{
    public class OrderQueriesUserCommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
