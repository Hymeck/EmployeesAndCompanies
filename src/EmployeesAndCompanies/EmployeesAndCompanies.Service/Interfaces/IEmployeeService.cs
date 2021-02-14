using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
    }
}