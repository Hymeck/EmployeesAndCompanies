using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<IReadOnlyCollection<string>> GetNamesAsync();
    }
}