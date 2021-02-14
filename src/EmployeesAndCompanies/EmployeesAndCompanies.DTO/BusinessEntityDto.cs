namespace EmployeesAndCompanies.DTO
{
    public record BusinessEntityDto(string Name, string Abbreviation)
    {
        public override string ToString() => Name;
    }
}