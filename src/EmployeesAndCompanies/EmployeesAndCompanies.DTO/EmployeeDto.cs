using System;
using System.Collections.Generic;

namespace EmployeesAndCompanies.DTO
{
    public class EmployeeDto
    {
        public int Id { get; init; }
        public string Name1 { get; init; }
        public string Name2 { get; init; }
        public string Name3 { get; init; }
        
        public DateTime EmploymentDate { get; init; }
        
        public IEnumerable<PostDto> Posts { get; init; }
        public IEnumerable<CompanyDto> Companies { get; init; }
        
        public string FullName => 
            string.Join(' ', Name2, Name1, Name3);

        public override string ToString() => FullName;
    }
}