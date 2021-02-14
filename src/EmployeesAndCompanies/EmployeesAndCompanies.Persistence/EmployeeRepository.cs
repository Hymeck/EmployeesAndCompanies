using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.Domain.Interfaces;

namespace EmployeesAndCompanies.Persistence
{
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            var query =
                $"insert into {EmployeeTable.TableName} ({EmployeeTable.Name1}, {EmployeeTable.Name2}, {EmployeeTable.Name3}, {EmployeeTable.EmploymentDate}) " +
                "values (@name1, @name2, @name3, @date) " +
                "SELECT SCOPE_IDENTITY()";

            SqlParameter[] parameters =
            {
                new("@name1", entity.Name1),
                new("@name2", entity.Name2),
                new("@name3", entity.Name3),
                new("@date", entity.EmploymentDate),
            };
            var id = Convert.ToInt32(
                await SqlHelper.ExecuteScalarAsync(ConnectionString, query, parameters: parameters));
            entity.Id = id;

            return entity;
        }

        public async Task<bool> UpdateAsync(Employee entity)
        {
            var query = $"update {EmployeeTable.TableName} set " +
                        $"{EmployeeTable.Name1} = @name1, " +
                        $"{EmployeeTable.Name2} = @name2, " +
                        $"{EmployeeTable.Name3} = @name3, " +
                        $"{EmployeeTable.EmploymentDate} = @date " +
                        $"where {EmployeeTable.Id} = @id";

            SqlParameter[] parameters =
            {
                new("@name1", entity.Name1),
                new("@name2", entity.Name2),
                new("@name3", entity.Name3),
                new("@date", entity.EmploymentDate),
                new("@id", entity.Id)
            };

            var affectedRowsCount =
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            return affectedRowsCount == 1;
        }

        public async Task<bool> DeleteAsync(int id) =>
            await base.DeleteAsync(EmployeeTable.TableName, EmployeeTable.Id, id);

        public async Task<Employee> FindAsync(int id)
        {
            var reader = await base.FromFind(EmployeeTable.TableName, EmployeeTable.Id, id);

            if (!reader.HasRows)
                return Employee.Empty;

            await reader.ReadAsync();
            //todo: add select from m2m table
            return new Employee
            {
                Id = reader.GetInt32(0),
                Name1 = reader.GetString(1),
                Name2 = reader.GetString(2),
                Name3 = reader.GetString(3),
                EmploymentDate = reader.GetDateTime(4)
            };
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = GetSelectAllString(EmployeeTable.TableName);
            var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);

            if (!reader.HasRows)
                return Enumerable.Empty<Employee>();

            var entities = new List<Employee>();
            while (await reader.ReadAsync())
            {
                entities.Add(new Employee
                {
                    Id = reader.GetInt32(0),
                    Name1 = reader.GetString(1),
                    Name2 = reader.GetString(2),
                    Name3 = reader.GetString(3),
                    EmploymentDate = reader.GetDateTime(4)
                });
            }

            return entities;
        }
    }
}