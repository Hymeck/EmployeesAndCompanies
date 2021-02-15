namespace EmployeesAndCompanies.DTO
{
    public record CompanyDto(int Id, string Name, int BusinessEntityId, int Size)
    {
        public override string ToString() => $"\"{Name}\"";
    }
}