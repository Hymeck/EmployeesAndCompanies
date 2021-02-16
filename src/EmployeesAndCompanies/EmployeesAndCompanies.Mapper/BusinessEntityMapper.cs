using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Mapper
{
    public class BusinessEntityMapper
    {
        public static BusinessEntityDto ToDto(BusinessEntity b) =>
            new(b.Id, b.Name, b.Abbreviation);

        public static BusinessEntity FromDto(BusinessEntityDto dto) =>
            new() {Id = dto.Id, Name = dto.Name, Abbreviation = dto.Abbreviation};
    }
}