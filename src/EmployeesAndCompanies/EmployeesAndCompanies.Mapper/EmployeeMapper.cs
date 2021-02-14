using System.Collections.Generic;
using System.Linq;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Mapper
{
    public class EmployeeMapper
    {
        // todo: what about posts and companies?
        public static EmployeeDto From(Employee e) => new()
        {
            Id = e.Id,
            Name1 = e.Name1,
            Name2 = e.Name2,
            Name3 = e.Name3,
            EmploymentDate = e.EmploymentDate
        };

        public static EmployeeDto From(Employee e, IEnumerable<Post> posts, IEnumerable<Company> companies)
        {
            var dto = From(e);
            dto.Posts = posts.Select(p => p.Name);
            dto.Companies = companies.Select(c => c.Name);
            return dto;
        }

        public static Employee To(EmployeeDto dto) => new()
        {
            Name1 = dto.Name1,
            Name2 = dto.Name2,
            Name3 = dto.Name3,
            EmploymentDate = dto.EmploymentDate
        };
    }
}