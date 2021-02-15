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

        public CompanyService(ICompanyRepository companyRepository) =>
            _companyRepository = companyRepository;

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
    }
}