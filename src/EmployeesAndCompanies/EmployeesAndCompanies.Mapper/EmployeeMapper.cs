using System.Collections.Generic;
using System.Linq;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Mapper
{
    public class EmployeeMapper
    {
        public static EmployeeDto ToDto(Employee e, IEnumerable<Post> posts, IEnumerable<Company> companies) =>
            new()
            {
                Id = e.Id,
                Name1 = e.Name1,
                Name2 = e.Name2,
                Name3 = e.Name3,
                EmploymentDate = e.EmploymentDate,
                Companies = companies.Select(CompanyMapper.ToDto),
                Posts = posts.Select(PostMapper.ToDto)
            };

        public static EmployeeDto ToDto(Employee e) =>
            ToDto(e, Enumerable.Empty<Post>(), Enumerable.Empty<Company>());

        public static Employee FromDto(EmployeeDto dto) => new()
        {
            Id = dto.Id,
            Name1 = dto.Name1,
            Name2 = dto.Name2,
            Name3 = dto.Name3,
            EmploymentDate = dto.EmploymentDate,
            Companies = dto.Companies.Select(CompanyMapper.FromDto),
            Posts = dto.Posts.Select(PostMapper.FromDto)
        };
    }
}