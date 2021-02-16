using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EmployeesAndCompanies.Domain.Entities;
using EmployeesAndCompanies.Persistence;
using static System.Console;

namespace EmployeesAndCompanies.Playground
{
    class Playground
    {
        private const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=employees_and_companies_db;Integrated Security=True;";

        private static async Task Main(string[] args)
        {
            // await PlayWithDb();
            // await Names();
            // await EmployeeCompanies();
            // await EmployeePosts();
            // await AddEmployeeWithCompaniesAndPosts();
            await GetAllBusinessEntities();
        }

        private static async Task GetAllBusinessEntities()
        {
            var repo = new BusinessEntityRepository(ConnectionString);
            var entities = await repo.GetAllAsync();
            WriteLine(string.Join('\n', entities));
        }
        
        private static async Task AddEmployeeWithCompaniesAndPosts()
        {
            var company1 = new Company { Id = 1};
            var company2 = new Company { Id = 2};

            var post1 = new Post {Id = 1};
            var post2 = new Post {Id = 2};

            var employee = new Employee
            {
                Name1 = "Name111",
                Name2 = "Name222",
                Name3 = "Name333",
                EmploymentDate = DateTime.Now,
                Companies = new[] {company1, company2},
                Posts = new[] {post1, post2}
            };

            var repo = new EmployeeRepository(ConnectionString);

            await repo.AddAsync(employee);
        }

        private static async Task EmployeePosts()
        {
            var employeeRepo = new EmployeeRepository(ConnectionString);
            var posts = await employeeRepo.GetPostsAsync(1);
            WriteLine(string.Join('\n', posts));
        }
        
        private static async Task EmployeeCompanies()
        {
            var employeeRepo = new EmployeeRepository(ConnectionString);
            var companies = await employeeRepo.GetCompaniesAsync(1);
            WriteLine(string.Join('\n', companies));
        }
        
        private static async Task Names()
        {
            var companyRepo = new CompanyRepository(ConnectionString);
            var names = await companyRepo.GetNamesAsync();
            WriteLine(string.Join('\n', names));
        }
        
        private static async Task PlayWithDb()
        {
            var companyRepo = new CompanyRepository(ConnectionString);
            foreach(var e in await companyRepo.GetAllAsync())
                WriteLine(e);
            WriteLine("---");
            var employeeRepo = new EmployeeRepository(ConnectionString);
            foreach(var e in await employeeRepo.GetAllAsync())
                WriteLine(e);
            
            var employee1 = new Employee
            {
                Name1 = "Вадим",
                Name2 = "Ярень",
                EmploymentDate = DateTime.Now
            };
            
            await employeeRepo.AddAsync(employee1);
            
            var employee2 = new Employee
            {
                Name1 = "Рей",
                Name2 = "Ером",
                EmploymentDate = DateTime.Now
            };
            
            await employeeRepo.AddAsync(employee2);
            
            foreach(var e in await employeeRepo.GetAllAsync())
                WriteLine(e);
            
            WriteLine();
            
            employee2.Name1 = "Джим";
            
            await employeeRepo.UpdateAsync(employee2);
            
            foreach(var e in await employeeRepo.GetAllAsync())
                WriteLine(e);
            
            WriteLine();
            
            await employeeRepo.DeleteAsync(2);
            
            foreach(var e in await employeeRepo.GetAllAsync())
                WriteLine(e);
        }
        
        private static void PrintRows(IEnumerable<DataRow> rows)
        {
            foreach (var a in rows)
                WriteLine(string.Join(' ', a.ItemArray));
        }

        private static async Task<DataRow[]> SelectAsync(string tableName)
        {
            var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();
            
            var select = $"SELECT * FROM {tableName}";
            var adapter = new SqlDataAdapter(select, connection);
            var dataset = new DataSet();
            adapter.Fill(dataset, tableName);
            var table = dataset.Tables[tableName];
            await connection.CloseAsync();
            return table.Select();
        }
    }
}