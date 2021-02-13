using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesAndCompanies.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public static readonly Employee Empty = new();
        public string Name1 { get; set; } = string.Empty;
        public string Name2 { get; set; } = string.Empty;
        public string Name3 { get; set; } = string.Empty;
        public DateTime EmploymentDate { get; set; }

        public IEnumerable<Post> Posts { get; set; } = Enumerable.Empty<Post>();
        public IEnumerable<Post> Companies { get; set; } = Enumerable.Empty<Post>();

        public override string ToString() =>
            $"{Id}. {Name1.Trim()} {Name2.Trim()} {Name3.Trim()} {EmploymentDate.ToShortDateString()}";
    }
}