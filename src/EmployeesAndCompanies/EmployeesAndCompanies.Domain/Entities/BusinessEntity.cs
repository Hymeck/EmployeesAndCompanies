using System.ComponentModel.DataAnnotations;

namespace EmployeesAndCompanies.Domain.Entities
{
    public class BusinessEntity : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        
        [MaxLength(10)]
        public string Abbreviation { get; set; }
    }
}