using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.Domain.Interfaces
{
    public interface IBusinessEntityRepository
    {
        Task<IEnumerable<BusinessEntity>> GetAllAsync();
        Task<BusinessEntity> FindAsync(int id);
    }
}