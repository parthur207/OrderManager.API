using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Domain.Enuns;

namespace OrderManager.API.Controllers.AdminControllers
{
    [ApiController]
    [Route("api/queries/admin")]
    public class AdminQueriesController : Controller
    {
        [Authorize(Roles = nameof(RoleEnum.Adm))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
