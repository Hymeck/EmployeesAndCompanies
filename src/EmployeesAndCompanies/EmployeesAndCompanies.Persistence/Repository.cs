using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmployeesAndCompanies.Persistence
{
    public abstract class Repository
    {
        public readonly string ConnectionString;

        protected Repository(string connectionString) =>
            ConnectionString = connectionString;

        protected static string GetSelectAllString(string tableName) =>
            $"select * from {tableName}";
        
        protected static string GetDeleteString(string tableName, string id) =>
            $"delete from {tableName} where {id} = @id";

        protected async Task<bool> DeleteAsync(string tableName, string idName, int id)
        {
            var query = GetDeleteString(tableName, idName);
            SqlParameter[] parameters = {new("@id", id)};

            var affectedRowsCount =
                await SqlHelper.ExecuteNonQueryAsync(ConnectionString, query, parameters: parameters);
            return affectedRowsCount == 1;
        }

        protected async Task<SqlDataReader> FromFind(string tableName, string idName, int id)
        {
            var query = GetSelectAllString(tableName) + $" where {idName} = @id";
            SqlParameter[] parameters = {new("@id", id)};
            return await SqlHelper.ExecuteReaderAsync(ConnectionString, query, parameters: parameters);
        }
    }
}