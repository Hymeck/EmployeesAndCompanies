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
            var companies = await _companyService.GetAllAsync();
            var companiesList = companies.ToSelectListItemList();
            
            ViewData["title"] = "Добавление нового сотрудника";

            var vm = EmployeeViewModel.EmptyWithCompanies(companiesList);
            return View("Employee", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] EmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = EmployeeViewModel.ToDto(vm);
                var result = await _employeeService.AddAsync(dto);
                if (result)
                    return RedirectToAction("Index");
            }

            return await Add();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _employeeService.GetAsync(id);
            
            var companies = await _companyService.GetAllAsync();
            // var companiesList = companies.ToSelectListItemList();
            var companiesList = companies.ToSelectListItemList(entity.Companies);
            
            ViewData["title"] = "Редактирование сотрудника";

            // todo: set checkbox if specified employee is in a company (may be several)
            
            var vm = EmployeeViewModel.FromDto(entity, companiesList);
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
            await _employeeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCompany(int id, int companyId)
        {
            await _employeeService.RemoveCompanyAsync(id, companyId);
            return await Edit(id);
        }
    }
}