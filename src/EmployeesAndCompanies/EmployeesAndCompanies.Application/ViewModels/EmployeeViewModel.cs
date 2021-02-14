using System;
using System.ComponentModel.DataAnnotations;

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
    }
}