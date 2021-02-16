#nullable enable
using System.Collections.Generic;
using System.Linq;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Application.Other
{
    public static class BusinessEntityExtensions
    {
        public static BusinessEntityDto? FindBusinessEntity(this IEnumerable<BusinessEntityDto> source, string businessEntityName) =>
            source
                .Where(e => 
                    e.Name == businessEntityName || 
                    e.Abbreviation == businessEntityName)
                .Select(x => x)
                .FirstOrDefault();
    }
}