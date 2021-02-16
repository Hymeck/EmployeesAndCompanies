namespace EmployeesAndCompanies.DTO
{
    public class CompanyDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public BusinessEntityDto BusinessEntity { get; init; }
        public int BusinessEntityId { get; init; }
        public int Size { get; init; }

        public CompanyDto()
        {
        }

        public CompanyDto(int id, string name, int businessEntityId, int size)
        {
            Id = id;
            Name = name;
            BusinessEntityId = businessEntityId;
            Size = size;
        }

        public override string ToString() => $"\"{Name}\"";
    }
}