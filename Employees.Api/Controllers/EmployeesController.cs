using System.Net;
using Employees.Api.Dtos;
using Employees.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServices _services;

        public EmployeesController(IEmployeeServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _services.GetEmployees();
            if (!employees.Any())
            {
                return NotFound("Employees not found");
            }

            return Ok(employees);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromQuery] int id)
        {
            var employee = await _services.GetEmployeeById(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            var response = await _services.AddEmployee(employeeDto);

            return !response
                ? StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error creating employee" })
                : Ok(new { Message = "Employee created", HttpCode = HttpStatusCode.OK });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var response = await _services.UpdateEmployee(employeeDto);

            return !response
                ? StatusCode(StatusCodes.Status500InternalServerError, new {message = "Error updating employee"})
                : NoContent();
        }
    }
}