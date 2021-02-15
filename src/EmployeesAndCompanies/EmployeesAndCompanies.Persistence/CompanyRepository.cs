using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.Domain.Interfaces;

namespace EmployeesAndCompanies.Persistence
{
    public class CompanyRepository : Repository, ICompanyRepository
    {
        public CompanyRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Company> AddAsync(Company entity)
        {
            var query =
                $"insert into {CompanyTable.TableName} ({CompanyTable.Name}, {CompanyTable.BusinessEntityId}, {CompanyTable.Size}) " +
                "values (@name, @businessId, @size) " +
                "SELECT SCOPE_IDENTITY()";

            SqlParameter[] parameters =
            {
                new("@name", entity.Name),
                new("@businessId", entity.BusinessEntityId),
                new("@size", entity.Size)
            };
            var id = Convert.ToInt32(
                await SqlHelper.ExecuteScalarAsync(ConnectionString, query, parameters: parameters));
            entity.Id = id;

            return entity;
        }

        public async Task<bool> UpdateAsync(Company entity)
        {
            var query = $"update {CompanyTable.TableName} set " +
                        $"{CompanyTable.Name} = @name, " +
                        $"{CompanyTable.BusinessEntityId} = @businessId, " +
                        $"{CompanyTable.Size} = @size " +
                        $"where {CompanyTable.Id} = @id";

            SqlParameter[] parameters =
            {
                new("@name", entity.Name),
                new("@businessId", entity.BusinessEntityId),
                new("@size", entity.Size),
                new("@id", entity.Id)
            };
            
            var affectedRowsCount = await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            return affectedRowsCount == 1;
        }

        public async Task<bool> DeleteAsync(int id) =>
            await base.DeleteAsync(CompanyTable.TableName, CompanyTable.Id, id);

        public async Task<Company> FindAsync(int id)
        {
            await using var reader = await FromFind(CompanyTable.TableName, CompanyTable.Id, id);

            if (!reader.HasRows)
                return Company.Empty;

            await reader.ReadAsync();
            return new Company
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                BusinessEntityId = reader.GetInt32(2),
                Size = reader.GetInt32(3)
            };
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var query = GetSelectAllString(CompanyTable.TableName);
            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);

            if (!reader.HasRows)
                return Enumerable.Empty<Company>();

            var entities = new Queue<Company>();
            while (await reader.ReadAsync())
            {
                entities.Enqueue(new Company
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    BusinessEntityId = reader.GetInt32(2),
                    Size = reader.GetInt32(3)
                });
            }

            return entities;
        }

        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            var query = $"select {CompanyTable.Name} from {CompanyTable.TableName}";
            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);
            if (!reader.HasRows)
                return Enumerable.Empty<string>();

            var names = new Queue<string>();
            while (await reader.ReadAsync()) 
                names.Enqueue(reader.GetString(0));
            
            return names;
        }
    }
}