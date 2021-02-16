using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.DTO;
using EmployeesAndCompanies.Mapper;
using EmployeesAndCompanies.Service.Interfaces;

namespace EmployeesAndCompanies.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IBusinessEntityRepository _businessEntityRepository;

        public CompanyService(ICompanyRepository companyRepository, IBusinessEntityRepository businessEntityRepository)
        {
            _companyRepository = companyRepository;
            _businessEntityRepository = businessEntityRepository;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var entities = await _companyRepository
                .GetAllAsync();
            
            return entities.Select(CompanyMapper.ToDto);
        }

        public async Task<IReadOnlyCollection<string>> GetNamesAsync()
        {
            var names = await _companyRepository
                .GetNamesAsync();
            
            return names.ToImmutableArray();
        }

        public async Task<bool> AddAsync(CompanyDto dto)
        {
            var entity = CompanyMapper.FromDto(dto);
            var result = await _companyRepository.AddAsync(entity);
            
            return result.Id != 0;
        }

        public async Task<bool> EditAsync(CompanyDto dto)
        {
            if (dto.Id == 0)
                return false;
            
            var entity = CompanyMapper.FromDto(dto); 
            return await _companyRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0)
                return false;
            
            return await _companyRepository.DeleteAsync(id);
        }

        public async Task<CompanyDto> GetAsync(int id)
        {
            var entity = await _companyRepository.FindAsync(id);
            var businessEntity = await _businessEntityRepository.FindAsync(entity.BusinessEntityId);
            return CompanyMapper.ToDto(entity, businessEntity);
        }
    }
}