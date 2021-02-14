using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAndCompanies.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<IReadOnlyCollection<string>> GetNamesAsync();
    }
}