using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.Domain.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IEnumerable<Company>> GetAllAsync();
    }
}