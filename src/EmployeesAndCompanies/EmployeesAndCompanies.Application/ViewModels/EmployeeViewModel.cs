using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EmployeesAndCompanies.Application.Other;
using EmployeesAndCompanies.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesAndCompanies.Application.ViewModels
{
    public class EmployeeViewModel
    {
        public static readonly EmployeeViewModel Empty = new();
        public int Id { get; init; }

        [Required] [Display(Name = "Имя")] 
        public string Name1 { get; set; } = string.Empty;

        [Required] [Display(Name = "Фамилия")] 
        public string Name2 { get; set; } = string.Empty;

        [Display(Name = "Отчество")] 
        public string Name3 { get; set; } = string.Empty;

        [DataType(DataType.Date)] 
        public DateTime EmploymentDate { get; set; } = DateTime.Now;

        // [Display(Name = "Компании")] 
        // public IEnumerable<CompanyDto> Companies { get; set; } = Enumerable.Empty<CompanyDto>();
        //
        // [Display(Name = "Должности")] 
        // public IEnumerable<PostDto> Posts { get; set; } = Enumerable.Empty<PostDto>();
        
        public IList<SelectListItem> Companies { get; set; } = ArraySegment<SelectListItem>.Empty;

        // public string GetCompaniesString() => string.Join(", ", Companies);

        public static EmployeeViewModel FromDto(EmployeeDto e, IList<SelectListItem> items) =>
            new()
            {
                Id = e.Id,
                Name1 = e.Name1,
                Name2 = e.Name2,
                Name3 = e.Name3,
                EmploymentDate = e.EmploymentDate,
                Companies = items
                // Companies = e.Companies.ToSelectListItemList(),
                // Posts = e.Posts
            };

        public static EmployeeDto ToDto(
            EmployeeViewModel vm,
            IEnumerable<CompanyDto> companies,
            IEnumerable<PostDto> posts) =>
            new()
            {
                Id = vm.Id,
                Name1 = vm.Name1,
                Name2 = vm.Name2,
                Name3 = vm.Name3,
                EmploymentDate = vm.EmploymentDate,
                Companies = companies,
                Posts = posts
            };

        public static EmployeeDto ToDto(EmployeeViewModel vm) =>
            // ToDto(vm, vm.Companies, vm.Posts);
            ToDto(vm, vm.Companies.Select(i => 
                new CompanyDto(Convert.ToInt32(i.Value), i.Text, 0, 0)), Enumerable.Empty<PostDto>());

        public static EmployeeViewModel EmptyWithCompanies(IList<SelectListItem> companies) => 
            new() {Companies = companies};
    }
}