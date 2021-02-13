namespace EmployeesAndCompanies.Persistence
{
    public abstract class Repository
    {
        public readonly string ConnectionString;

        protected Repository(string connectionString) =>
            ConnectionString = connectionString;
    }
}