using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
