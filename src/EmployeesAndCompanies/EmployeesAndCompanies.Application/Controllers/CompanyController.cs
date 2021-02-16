using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Application.Other;
using EmployeesAndCompanies.Application.ViewModels;
using EmployeesAndCompanies.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAndCompanies.Application.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IBusinessEntityService _businessEntityService;

        public CompanyController(ICompanyService companyService, IBusinessEntityService businessEntityService)
        {
            _companyService = companyService;
            _businessEntityService = businessEntityService;
        }

        public async Task<IActionResult> Index() =>
            View(await _companyService.GetAllAsync());

        [HttpGet]
        // public async Task<IActionResult> Add()
        public IActionResult Add()
        {
            ViewData["title"] = "Добавление компании";
            // var businessEntities = await _businessEntityService.GetAllAsync();
            // var list = businessEntities.ToSelectList();
            // return View("Company", CompanyViewModel.EmptyWithBusinessEntities(list));
            
            return View("Company", CompanyViewModel.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var businessEntities = await _businessEntityService.GetAllAsync();
                var businessEntity = businessEntities.FindBusinessEntity(vm.BusinessEntityName);
                
                if (businessEntity == null)
                    return View("Company", vm);
                
                var dto = CompanyViewModel.ToDto(vm, businessEntity);
                var result = await _companyService.AddAsync(dto);
                
                if (result)
                    return RedirectToAction("Index");
            }

            return View("Company", vm);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["title"] = "Редактирование компании";
            var entity = await _companyService.GetAsync(id);
            var vm = CompanyViewModel.FromDto(entity);
            return View("Company", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var businessEntities = await _businessEntityService.GetAllAsync();
                var businessEntity = businessEntities.FindBusinessEntity(vm.BusinessEntityName);
                
                if (businessEntity == null)
                    return View("Company", vm);
                
                var dto = CompanyViewModel.ToDto(vm, businessEntity);
                var result = await _companyService.EditAsync(dto);
                if (result)
                    return RedirectToAction("Index");
            }

            return View("Company", vm);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}