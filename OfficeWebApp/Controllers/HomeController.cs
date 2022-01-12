using Microsoft.AspNetCore.Mvc;
using OfficeWebApp.Models;
using OfficeWebApp.ViewModels;

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
