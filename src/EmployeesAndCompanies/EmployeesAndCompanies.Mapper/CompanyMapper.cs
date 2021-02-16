using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Mapper
{
    public class CompanyMapper
    {
        public static CompanyDto ToDto(Company c) =>
            new(c.Id, c.Name, c.BusinessEntityId, c.Size);

        public static CompanyDto ToDto(Company c, BusinessEntity e) =>
            new(c.Id, c.Name, e.Id, c.Size)
                {BusinessEntity = BusinessEntityMapper.ToDto(e)};
        
        public static Company FromDto(CompanyDto dto, BusinessEntity e) =>
            new()
            {
                Id = dto.Id,
                Name = dto.Name,
                BusinessEntityId = dto.Id,
                BusinessEntity = e,
                Size = dto.Size
            };

        public static Company FromDto(CompanyDto dto) =>
            FromDto(dto, BusinessEntity.Empty);
    }
}