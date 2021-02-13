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