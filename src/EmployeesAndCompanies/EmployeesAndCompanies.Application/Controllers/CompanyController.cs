using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index() => 
            View();
    }
}