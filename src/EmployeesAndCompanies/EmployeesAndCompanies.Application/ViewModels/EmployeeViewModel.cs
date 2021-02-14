using System;
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

        [Required] 
        [Display(Name = "Имя")] 
        public string Name1 { get; set; } = string.Empty;

        [Required] 
        [Display(Name = "Фамилия")] 
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

        public static EmployeeViewModel From(EmployeeDto e) =>
            new()
            {
                Id = e.Id,
                Name1 = e.Name1,
                Name2 = e.Name2,
                Name3 = e.Name3,
                EmploymentDate = e.EmploymentDate,
                Companies = e.Companies,
                Posts = e.Posts
            };
    }
}