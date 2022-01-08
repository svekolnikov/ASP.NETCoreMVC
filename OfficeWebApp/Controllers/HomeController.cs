using Microsoft.AspNetCore.Mvc;

namespace OfficeWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
