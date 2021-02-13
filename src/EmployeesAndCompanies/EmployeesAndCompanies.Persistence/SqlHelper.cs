using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmployeesAndCompanies.Persistence
{
    public static class SqlHelper
    {
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string query,
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand(query, connection)
                {CommandType = commandType};

            command.Parameters.AddRange(parameters);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public static async Task<object> ExecuteScalarAsync(string connectionString, string query,
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand(query, connection)
                {CommandType = commandType};
            command.Parameters.AddRange(parameters);

            await connection.OpenAsync();
            return await command.ExecuteScalarAsync();
        }

        public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, string query,
            CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(connectionString);

            await using var command = new SqlCommand(query, connection) 
                {CommandType = commandType};
            command.Parameters.AddRange(parameters);

            await connection.OpenAsync();  
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
    }
}