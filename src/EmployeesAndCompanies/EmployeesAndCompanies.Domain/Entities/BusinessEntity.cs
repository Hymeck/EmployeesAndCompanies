namespace EmployeesAndCompanies.Domain.Entities
{
    public class BusinessEntity : BaseEntity
    {
        public static readonly BusinessEntity Empty = new();
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public override string ToString() => $"{Id}. {Name} {Abbreviation}";
    }
}