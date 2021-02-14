using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.DTO;
using EmployeesAndCompanies.Service.Interfaces;

namespace EmployeesAndCompanies.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) => 
            _employeeRepository = employeeRepository;

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var entities = await _employeeRepository.GetAllAsync();
            return entities.Select(EmployeeDto.From);
        }
    }
}