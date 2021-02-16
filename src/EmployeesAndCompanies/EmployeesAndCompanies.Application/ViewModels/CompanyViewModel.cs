using System.ComponentModel.DataAnnotations;
using EmployeesAndCompanies.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesAndCompanies.Application.ViewModels
{
    public class CompanyViewModel
    {
        public static readonly CompanyViewModel Empty = new();
        public int Id { get; init; }

        [Required] [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        [Required] [Display(Name = "Штат")]
        public int Size { get; set; }

        [Required] [Display(Name = "Организационно-правовая форма")]
        public string BusinessEntityName { get; set; } = string.Empty;
        public BusinessEntityDto BusinessEntity { get; set; }
        public int BusinessEntityId { get; set; }

        public SelectList BusinessEntities { get; set; }
        
        public static CompanyDto ToDto(CompanyViewModel vm) =>
            new(vm.Id, vm.Name, vm.BusinessEntityId, vm.Size);

        public static CompanyDto ToDto(CompanyViewModel vm, BusinessEntityDto dto) =>
            new(vm.Id, vm.Name, dto.Id, vm.Size);
        
        public static CompanyViewModel FromDto(CompanyDto dto) =>
            new()
            {
                Id = dto.Id,
                Name = dto.Name,
                BusinessEntityId = dto.BusinessEntityId,
                BusinessEntity = dto.BusinessEntity,
                BusinessEntityName = dto.BusinessEntity?.Name
            };

        public static CompanyViewModel EmptyWithBusinessEntities(SelectList businessEntities) =>
            new() {BusinessEntities = businessEntities};
    }
}