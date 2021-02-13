using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<TEntity> FindAsync(int id);
    }
}