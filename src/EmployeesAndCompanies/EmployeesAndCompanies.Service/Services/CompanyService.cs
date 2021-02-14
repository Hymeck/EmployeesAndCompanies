using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.Service.Interfaces;

namespace EmployeesAndCompanies.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository) =>
            _companyRepository = companyRepository;

        public async Task<IReadOnlyCollection<string>> GetNamesAsync() =>
            (await _companyRepository
                .GetNamesAsync())
            .ToImmutableArray();
    }
}