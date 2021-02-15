﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EmployeesAndCompanies.DTO;

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

        [Display(Name = "Компании")] 
        public IEnumerable<string> Companies { get; set; } = Enumerable.Empty<string>();

        [Display(Name = "Должности")] 
        public IEnumerable<string> Posts { get; set; } = Enumerable.Empty<string>();

        public string GetCompaniesString() => string.Join(", ", Companies);

        public static EmployeeViewModel FromDto(EmployeeDto e) =>
            new()
            {
                Id = e.Id,
                Name1 = e.Name1,
                Name2 = e.Name2,
                Name3 = e.Name3,
                EmploymentDate = e.EmploymentDate,
                Companies = e.Companies.Select(c => c.Name),
                Posts = e.Posts.Select(p => p.Name)
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
            ToDto(vm, Enumerable.Empty<CompanyDto>(), Enumerable.Empty<PostDto>());
    }
}