namespace EmployeesAndCompanies.Domain.Entities
{
    public class Company : BaseEntity
    {
        public static readonly Company Empty = new();
        public string Name { get; set; } = "";
        public int BusinessEntityId { get; set; }
        public BusinessEntity BusinessEntity { get; set; } = BusinessEntity.Empty;
        public int Size { get; set; }

        public override string ToString() =>
            $"{Id}. \"{Name}\" {BusinessEntityId} {Size}";
    }
}