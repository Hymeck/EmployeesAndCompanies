using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.Domain.Interfaces;

namespace EmployeesAndCompanies.Persistence
{
    public class BusinessEntityRepository : Repository, IBusinessEntityRepository
    {
        public BusinessEntityRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<BusinessEntity>> GetAllAsync()
        {
            var query = GetSelectAllString("business_entity");
            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);

            if (!reader.HasRows)
                return Enumerable.Empty<BusinessEntity>();

            var entities = new Queue<BusinessEntity>();
            while (await reader.ReadAsync())
            {
                entities.Enqueue(new BusinessEntity
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Abbreviation = reader.GetString(2)
                });
            }

            return entities;
        }

        public async Task<BusinessEntity> FindAsync(int id)
        {
            await using var reader = await base.FromFind("business_entity", "id", id);
            if (!reader.HasRows)
                return BusinessEntity.Empty;

            await reader.ReadAsync();
            
            return new BusinessEntity
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Abbreviation = reader.GetString(2),
            };
        }
    }
}