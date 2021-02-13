using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
            foreach(var c in await companyRepo.GetAll())
                WriteLine(c);
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