﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                "select SCOPE_IDENTITY()";
            
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
            
            var queueParams = new Queue<SqlParameter>();

            var i = 0;
            var sb = new StringBuilder();
            foreach (var c in entity.Companies)
            {
                sb.Append($"(@eId{i}, @cId{i}), ");
                queueParams.Enqueue(new SqlParameter($"@eId{i}", id));
                queueParams.Enqueue(new SqlParameter($"@cId{i}", c.Id));
                i++;
            }

            if (i != 0)
            {
                sb.Remove(sb.Length - 2, 2);
                query = $"insert into m2m_empl_comp values {sb}";
                parameters = queueParams.ToArray();
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
                
                i = 0;
                sb = new StringBuilder();
                queueParams = new Queue<SqlParameter>();
            }
            
            foreach (var p in entity.Posts)
            {
                sb.Append($"(@eId{i}, @pId{i}), ");
                queueParams.Enqueue(new SqlParameter($"@eId{i}", id));
                queueParams.Enqueue(new SqlParameter($"@pId{i}", p.Id));
                i++;
            }

            if (i != 0)
            {
                sb.Remove(sb.Length - 2, 2);
                query = $"insert into m2m_empl_post values {sb}";
                parameters = queueParams.ToArray();
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            }
            
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

            var result = affectedRowsCount == 1;

            if (result)
            {
                // todo: update companies
                // todo: update posts
            }
            
            return result;
        }

        public async Task<bool> DeleteAsync(int id) =>
            await base.DeleteAsync(EmployeeTable.TableName, EmployeeTable.Id, id);

        public async Task<Employee> FindAsync(int id)
        {
            await using var reader = await base.FromFind(EmployeeTable.TableName, EmployeeTable.Id, id);

            if (!reader.HasRows)
                return Employee.Empty;

            await reader.ReadAsync();
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
            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query);

            if (!reader.HasRows)
                return Enumerable.Empty<Employee>();

            var entities = new Queue<Employee>();
            while (await reader.ReadAsync())
            {
                entities.Enqueue(new Employee
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

        public async Task<IEnumerable<Post>> GetPostsAsync(int employeeId)
        {
            var query = "select post.id, post.name " +
                        "from m2m_empl_post " +
                        "inner join employee on employee.id = m2m_empl_post.id_employee " +
                        "inner join post on m2m_empl_post.id_post = post.id " +
                        "where employee.id = @id";
            
            SqlParameter[] parameters = { new("@id", employeeId) };

            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query, parameters: parameters);
            if (!reader.HasRows)
                return Enumerable.Empty<Post>();

            var entities = new Queue<Post>();
            while (await reader.ReadAsync())
            {
                entities.Enqueue(new Post
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return entities;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(int employeeId)
        {
            var query = "select company.id, company.name, company.id_business_entity, company.size " +
                        "from m2m_empl_comp " +
                        "inner join employee on employee.id = m2m_empl_comp.id_employee " +
                        "inner join company on m2m_empl_comp.id_company = company.id " +
                        "where employee.id = @id";
            
            SqlParameter[] parameters = { new("@id", employeeId) };
            
            await using var reader = await SqlHelper.ExecuteReaderAsync(ConnectionString, query, parameters: parameters);
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

        public async Task<bool> RemoveCompany(int employeeId, int companyId)
        {
            var query = "delete from m2m_empl_comp where id_employee = @employeeId and id_company = @companyId";
            SqlParameter[] parameters =
            {
                new("@employeeId", employeeId),
                new("@companyId", companyId),
            };
            var affectedRowsCount =
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            
            return affectedRowsCount == 1;
        }
    }
}