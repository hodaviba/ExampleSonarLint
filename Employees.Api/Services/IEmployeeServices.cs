using Employees.Api.Dtos;
using Employees.Api.Models;

namespace Employees.Api.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetEmployees();

        Task<EmployeeDto> GetEmployeeById(int id);

        Task<bool> AddEmployee(EmployeeDto employeeDto);
    }
}
