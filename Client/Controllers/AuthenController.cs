using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthenController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
