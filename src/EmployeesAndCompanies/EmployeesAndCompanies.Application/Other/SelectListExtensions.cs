using System.Collections.Generic;
using System.Linq;
using EmployeesAndCompanies.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesAndCompanies.Application.Other
{
    public static class SelectListExtensions
    {
        public static IList<SelectListItem> ToSelectListItemList(this IEnumerable<CompanyDto> source) =>
            source.Select(dto =>
                new SelectListItem
                {
                    Text = dto.Name,
                    Value = dto.Id.ToString()
                }).ToArray();

        public static IList<SelectListItem> ToSelectListItemList(
            this IEnumerable<CompanyDto> companies,
            IEnumerable<CompanyDto> employeeCompanies)
        {
            var companyQueue = new Queue<SelectListItem>();
            // todo: make it more efficient? (O(n^2) -> O(n))
            foreach (var company in companies)
            {
                companyQueue.Enqueue(employeeCompanies.Any(employeeCompany => company.Id == employeeCompany.Id)
                    ? new SelectListItem(company.Name, company.Id.ToString(), selected: true)
                    : new SelectListItem(company.Name, company.Id.ToString(), selected: false));
            }

            return companyQueue.ToArray();
        }

        public static SelectList ToSelectList(this IEnumerable<BusinessEntityDto> source) =>
            new (source, "value", "text");
        
    }
}