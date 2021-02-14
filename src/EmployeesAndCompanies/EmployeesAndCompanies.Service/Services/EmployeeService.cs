using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Interfaces;
using EmployeesAndCompanies.DTO;
using EmployeesAndCompanies.Mapper;
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
            return entities.Select(EmployeeMapper.From);
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            var entity = await _employeeRepository.FindAsync(id);
            var posts = await _employeeRepository.GetPostsAsync(id);
            var companies = await _employeeRepository.GetCompaniesAsync(id);
            return EmployeeMapper.From(entity, posts, companies);
        }

        public async Task<bool> AddAsync(EmployeeDto dto)
        {
            var result = await _employeeRepository.AddAsync(EmployeeMapper.To(dto));
            return result.Id != 0;
        }
    }
}