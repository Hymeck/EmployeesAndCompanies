using System.ComponentModel.DataAnnotations;

namespace EmployeesAndCompanies.Domain.Entities
{
    public class Post : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
    }
}