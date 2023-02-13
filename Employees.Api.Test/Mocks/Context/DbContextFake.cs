using Employees.Api.Context;
using Employees.Api.Test.Mocks.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Employees.Api.Test.Mocks.Context
{
    public static class DbContextFake
    {
        public static async Task<EmployeeContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EmployeeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new EmployeeContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Employees.CountAsync() <= 0)
            {
                foreach (var employee in await EmployeeData.GetEmployees())
                {
                    databaseContext.Employees.Add(employee);
                }

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }
    }
}
