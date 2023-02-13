using Employees.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Api.Context
{
    public interface IEmployeeContext
    {
        DbSet<Employee> Employees { get; set; }

        Task<int> SaveChangesAsync();
    }
}
