using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Mapper
{
    public class PostMapper
    {
        public static PostDto ToDto(Post p) =>
            new(p.Id, p.Name);

        public static Post FromDto(PostDto dto) =>
            new() {Id = dto.Id, Name = dto.Name};
    }
}