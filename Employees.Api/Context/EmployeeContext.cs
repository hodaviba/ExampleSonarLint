using Employees.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Api.Context
{
    public class EmployeeContext : DbContext, IEmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
    }
}
