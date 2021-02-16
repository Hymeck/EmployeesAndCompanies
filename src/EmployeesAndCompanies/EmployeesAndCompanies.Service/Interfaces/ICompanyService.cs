using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<IReadOnlyCollection<string>> GetNamesAsync();
        Task<bool> AddAsync(CompanyDto dto);
        Task<bool> EditAsync(CompanyDto dto);
        Task<bool> DeleteAsync(int id);
        Task<CompanyDto> GetAsync(int id);
    }
}