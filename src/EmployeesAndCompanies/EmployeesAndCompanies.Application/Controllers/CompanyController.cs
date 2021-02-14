using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    [Route("companies")]
    public class CompanyController : Controller
    {
        public IActionResult Index() => 
            View();
    }
}