using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    [Route("employees")]
    public class EmployeeController : Controller
    {
        public IActionResult Index() => 
            View();
    }
}