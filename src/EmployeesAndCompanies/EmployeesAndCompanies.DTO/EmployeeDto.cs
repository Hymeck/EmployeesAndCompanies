using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesAndCompanies.DTO
{
    public class EmployeeDto
    {
        public int Id { get; init; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        
        public DateTime EmploymentDate { get; set; }
        
        public IEnumerable<string> Posts { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Companies { get; set; } = Enumerable.Empty<string>();
        
        public string FullName => 
            string.Join(' ', Name2.Trim(), Name1.Trim(), Name3.Trim());

        public override string ToString() => FullName;
    }
}