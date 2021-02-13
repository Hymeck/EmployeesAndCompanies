using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeesAndCompanies.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [StringLength(30)] 
        public string Name1 { get; set; }
        
        [StringLength(30)]
        public string Name2 { get; set; }
        
        [StringLength(30)] 
        public string Name3 { get; set; }
        
        [DataType(DataType.DateTime)] 
        public DateTime EmploymentDate { get; set; }
        
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Post> Companies { get; set; }
    }
}