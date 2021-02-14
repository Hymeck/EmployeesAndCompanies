using System.Threading.Tasks;
using EmployeesAndCompanies.Application.ViewModels;
using EmployeesAndCompanies.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) =>
            _employeeService = employeeService;

        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _employeeService.GetAllAsync());


        [HttpGet]
        public IActionResult Edit(int id)
        {
            // todo: request to DB
            return View("Employee", EmployeeViewModel.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel vm)
        {
            // todo: editing logic
            // if all is ok -> RedirectToAction("Index");
            return View("Employee", vm);
        }
        
        [HttpGet]
        public IActionResult Add() => 
            View("Employee", EmployeeViewModel.Empty);

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel vm)
        {
            // todo: adding logic
            return View("Employee", EmployeeViewModel.Empty);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            // todo: deleting logic
            return RedirectToAction("Index");
        }
    }
}