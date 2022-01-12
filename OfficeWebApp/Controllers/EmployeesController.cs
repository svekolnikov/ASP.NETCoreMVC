using Microsoft.AspNetCore.Mvc;
using OfficeWebApp.Models;
using OfficeWebApp.ViewModels;

namespace OfficeWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            var result = new EmployeesViewModel();

            for (var i = 1; i <= 10; i++)
            {
                result.Employees.Add(
                    new Employee()
                    {
                        Id = i,
                        FirstName = $"First Name Employe - {i}",
                        LastName = $"Last Name Employe - {i}"
                    });
            }

            return View(result);
        }
    }
}
