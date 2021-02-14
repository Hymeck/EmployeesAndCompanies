using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => 
            View();
    }
}