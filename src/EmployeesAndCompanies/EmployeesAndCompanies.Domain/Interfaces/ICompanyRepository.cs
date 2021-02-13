using System.Collections.Generic;
using EmployeesAndCompanies.Domain.Entities;

namespace EmployeesAndCompanies.Domain.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> GetAll();
    }
}