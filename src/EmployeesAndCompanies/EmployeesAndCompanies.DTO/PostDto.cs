namespace EmployeesAndCompanies.DTO
{
    public record PostDto(int Id, string Name)
    {
        public override string ToString() => Name;
    }
}