using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.DTO;

namespace EmployeesAndCompanies.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetAsync(int id);
        Task<bool> AddAsync(EmployeeDto dto);
        Task<bool> EditAsync(EmployeeDto dto);
    }
}