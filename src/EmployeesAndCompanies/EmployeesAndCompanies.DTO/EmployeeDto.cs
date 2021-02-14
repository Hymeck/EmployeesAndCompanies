using System;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.DTO
{
    public class EmployeeDto
    {
        public int Id { get; init; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; } = string.Empty;
        
        public DateTime EmploymentDate { get; init; }
        
        public string FullName => 
            string.Join(' ', Name2, Name1, Name3);

        public override string ToString() => FullName;
        
        // todo: what about posts and companies?
        public static EmployeeDto From(Employee e) => new()
        {
            Id = e.Id,
            Name1 = e.Name1,
            Name2 = e.Name2,
            Name3 = e.Name3,
            EmploymentDate = e.EmploymentDate
        };
    }
}