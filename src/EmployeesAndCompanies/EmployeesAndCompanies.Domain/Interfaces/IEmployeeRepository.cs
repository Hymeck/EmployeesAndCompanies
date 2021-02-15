using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Post>> GetPostsAsync(int employeeId);
        Task<IEnumerable<Company>> GetCompaniesAsync(int employeeId);
        Task<bool> RemoveCompany(int employeeId, int companyId);
    }
}