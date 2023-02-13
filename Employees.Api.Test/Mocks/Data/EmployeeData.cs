using Employees.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Api.Test.Mocks.Data
{
    internal static class EmployeeData
    {
        private static List<Employee> Employees => new List<Employee>()
        {
            new() { Id = 1, Name = "Juan", Age = 25 },
            new() { Id = 2, Name = "Pedro", Age = 38 }
        };

        public static async Task<List<Employee>> GetEmployees()
        {
            return await Task.Run(() => Employees);
        }

        public static async Task<List<Employee>> GetEmployeesEmpty()
        {
            return await Task.Run(() => new List<Employee>());
        }
    }
}
