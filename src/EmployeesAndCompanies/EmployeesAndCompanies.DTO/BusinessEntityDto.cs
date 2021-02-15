namespace EmployeesAndCompanies.DTO
{
    public record BusinessEntityDto(int Id, string Name, string Abbreviation)
    {
        public override string ToString() => Name;
    }
}