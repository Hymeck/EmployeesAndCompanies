using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.DTO;
using EmployeesAndCompanies.Mapper;
using EmployeesAndCompanies.Service.Interfaces;

namespace EmployeesAndCompanies.Service.Services
{
    public class BusinessEntityService : IBusinessEntityService
    {
        private readonly IBusinessEntityRepository _businessEntityRepository;

        public BusinessEntityService(IBusinessEntityRepository businessEntityRepository) => 
            _businessEntityRepository = businessEntityRepository;

        public async Task<IEnumerable<BusinessEntityDto>> GetAllAsync()
        {
            var entities = await _businessEntityRepository.GetAllAsync();
            return entities.Select(BusinessEntityMapper.ToDto);
        }
    }
}