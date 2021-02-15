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

        public static SelectList ToSelectList(this IEnumerable<string> source) =>
            new(source, "Value");
    }
}