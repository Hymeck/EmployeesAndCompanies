namespace EmployeesAndCompanies.DTO
{
    public record PostDto(string Name)
    {
        public override string ToString() => Name;
    }
}