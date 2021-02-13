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

        public async Task<Company> UpdateAsync(Company entity)
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
                new("@size", entity.Size)
            };

            var id = Convert.ToInt32(
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters));
            entity.Id = id;

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = $"delete from {CompanyTable.TableName} where {CompanyTable.Id} = @id";
            SqlParameter[] parameters = {new("@id", id)};

            var affectedRowsCount =
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            return affectedRowsCount == 1;
        }

        public async Task<Company> FindAsync(int id)
        {
            var query =
                $"select {CompanyTable.Id}, {CompanyTable.Name}, {CompanyTable.BusinessEntityId}, {CompanyTable.Size} from {CompanyTable.TableName} where {CompanyTable.Id} = @id";
            SqlParameter[] parameters = {new("@id", id)};
            var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query, parameters: parameters);

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

        public async Task<IEnumerable<Company>> GetAll()
        {
            var query =
                $"select {CompanyTable.Id}, {CompanyTable.Name}, {CompanyTable.BusinessEntityId}, {CompanyTable.Size} from {CompanyTable.TableName}";
            var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);

            if (!reader.HasRows)
                return Enumerable.Empty<Company>();

            var companies = new List<Company>();
            // todo: how it read more efficient?
            while (await reader.ReadAsync())
            {
                companies.Add(new Company
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    BusinessEntityId = reader.GetInt32(2),
                    Size = reader.GetInt32(3)
                });
            }

            return companies;
        }
    }
}