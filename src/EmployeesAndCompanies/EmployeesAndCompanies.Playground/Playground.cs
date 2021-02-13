using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeesAndCompanies.Persistence;
using static System.Console;

namespace EmployeesAndCompanies.Playground
{
    class Playground
    {
        static void Main(string[] args)
        {
            // string connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            string connectionString =
                @"Data Source=.\SQLEXPRESS;Initial Catalog=employees_and_companies_db;Integrated Security=True;";

            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                var tableName = CompanyTable.TableName;
                var rows = Select(connection, tableName);
                PrintRows(rows);

                AddCompany(connection, "Company2", 2, 8);
                // AddCompany(connection, "Added company 1", 1, 9);
                //
                rows = Select(connection, tableName);
                PrintRows(rows);
            }
            catch (SqlException ex)
            {
                WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                WriteLine("Подключение закрыто...");
            }
        }

        private static void PrintRows(IEnumerable<DataRow> rows)
        {
            foreach (var a in rows)
                WriteLine(string.Join(' ', a.ItemArray));
        }

        private static DataRow[] Select(SqlConnection connection, string tableName)
        {
            var select = $"SELECT * FROM {tableName}";
            var adapter = new SqlDataAdapter(select, connection);
            var dataset = new DataSet();
            adapter.Fill(dataset, tableName);
            var table = dataset.Tables[tableName];
            return table.Select();
        }

        private static void AddCompany(SqlConnection connection, string name, int businessId, int size)
        {
            var tableName = CompanyTable.TableName;
            // var select = $"SELECT * FROM {tableName}";

            // var adapter = new SqlDataAdapter(select, connection);
            // var set = new DataSet();
            // adapter.Fill(set, tableName);
            //
            // var table = set.Tables[tableName];
            //
            // var row = table.NewRow();            
            // row[CompanyTable.Name] = name;
            // row[CompanyTable.BusinessEntityId] = businessId;
            // row[CompanyTable.Size] = size;
            //
            // table.Rows.Add(row);
            //
            // //Updating Database Table
            // var builder = new SqlCommandBuilder(adapter);
            // adapter.Update(table);
            
            var query =
                $"insert into {tableName} ({CompanyTable.Name}, {CompanyTable.BusinessEntityId}, {CompanyTable.Size}) " +
                $"values ('{name}', {businessId}, {size})";
            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}