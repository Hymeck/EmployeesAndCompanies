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
            return entities.Select(EmployeeMapper.ToDto);
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            var entity = await _employeeRepository.FindAsync(id);
            var posts = await _employeeRepository.GetPostsAsync(id);
            var companies = await _employeeRepository.GetCompaniesAsync(id);
            return EmployeeMapper.ToDto(entity, posts, companies);
        }

        public async Task<bool> AddAsync(EmployeeDto dto)
        {
            var entity = EmployeeMapper.FromDto(dto);
            var result = await _employeeRepository.AddAsync(entity);
            return result.Id != 0;
        }

        public async Task<bool> EditAsync(EmployeeDto dto)
        {
            if (dto.Id == 0)
                return false;
            
            return await _employeeRepository.UpdateAsync(EmployeeMapper.FromDto(dto));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0)
                return false;
            
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<bool> RemoveCompanyAsync(int id, int companyId)
        {
            if (id == 0 || companyId == 0) 
                return false;
            
            return await _employeeRepository.RemoveCompany(id, companyId);
        }
    }
}