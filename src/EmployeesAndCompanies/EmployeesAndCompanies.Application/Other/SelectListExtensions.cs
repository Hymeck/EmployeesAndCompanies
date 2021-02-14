using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesAndCompanies.Application.Other
{
    public static class SelectListExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<string> source) => 
            new (source, "Value");
    }
}