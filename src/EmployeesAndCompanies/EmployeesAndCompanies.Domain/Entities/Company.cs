using System.ComponentModel.DataAnnotations;

namespace EmployeesAndCompanies.Domain.Entities
{
    public class Company : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public BusinessEntity BusinessEntity { get; set; }
        public int Size { get; set; }
    }
}