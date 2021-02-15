using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Application.Other;
using EmployeesAndCompanies.Application.ViewModels;
using EmployeesAndCompanies.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;

        public EmployeeController(IEmployeeService employeeService, ICompanyService companyService) =>
            (_employeeService, _companyService) = (employeeService, companyService);

        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _employeeService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["companies"] = (await _companyService.GetNamesAsync()).ToSelectList();
            ViewData["title"] = "Добавление нового сотрудника";

            return View("Employee", EmployeeViewModel.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // request to BD for companies and posts
                var result = await _employeeService.AddAsync(EmployeeViewModel.ToDto(vm));
                if (result)
                    return RedirectToAction("Index");
            }

            return await Add();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var names = await _companyService.GetNamesAsync();
            var entity = await _employeeService.GetAsync(id);
            ViewData["companies"] = names
                .Except(entity.Companies.Select(c => c.Name))
                .ToSelectList();
            ViewData["title"] = "Редактирование сотрудника";

            var vm = EmployeeViewModel.FromDto(entity);
            return View("Employee", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.EditAsync(EmployeeViewModel.ToDto(vm));
                if (result)
                    return RedirectToAction("Index");
            }

            return View("Employee", vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCompany(int id, string companyName)
        {
            throw new System.NotImplementedException();
        }
    }
}