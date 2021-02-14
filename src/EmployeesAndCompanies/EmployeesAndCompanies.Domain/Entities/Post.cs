namespace EmployeesAndCompanies.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Name { get; set; }
        public override string ToString() => 
            $"{Id}. {Name}";
    }
}