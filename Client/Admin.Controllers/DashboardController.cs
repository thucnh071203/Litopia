using Microsoft.AspNetCore.Mvc;

namespace Client.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
