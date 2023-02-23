using Employees.Api.Controllers;
using Employees.Api.Services;
using Employees.Api.Test.Mocks.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Employees.Api.Dtos;
using Xunit;

namespace Employees.Api.Test.Controllers
{
    public class EmployeesControllerTest
    {
        private readonly Mock<IEmployeeServices> employeeServices;

        public EmployeesControllerTest()
        {
            employeeServices = new Mock<IEmployeeServices>();
        }

        [Fact]
        public async Task When_GetEmployees_ShouldReturnOK()
        {
            //Arrange
            employeeServices.Setup(m => m.GetEmployees()).Returns(EmployeeData.GetEmployees);
            var controller = new EmployeesController(employeeServices.Object);

            //Act
            var response = await controller.GetEmployees();

            //Assert
            (response as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status200OK);
            employeeServices.Verify(m => m.GetEmployees());
        }

        [Fact]
        public async Task When_GetEmployees_ShouldReturnNotFound()
        {
            //Arrange
            employeeServices.Setup(m => m.GetEmployees()).Returns(EmployeeData.GetEmployeesEmpty);
            var controller = new EmployeesController(employeeServices.Object);

            //Act
            var response = await controller.GetEmployees();

            //Assert
            (response as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            employeeServices.Verify(m => m.GetEmployees());
        }

        [Fact]
        public async Task When_AddEmployee_ShouldReturnOK()
        {
            //Arrange
            employeeServices.Setup(m => m.AddEmployee(It.IsAny<EmployeeDto>())).Returns(Task.Run(() => true));
            var controller = new EmployeesController(employeeServices.Object);

            //Act
            var response = await controller.AddEmployee(new EmployeeDto());
            
            //Assert
            (response as ObjectResult)?.StatusCode.Should().Be(StatusCodes.Status200OK);
            employeeServices.Verify(m => m.AddEmployee(It.IsAny<EmployeeDto>()));
        }
    }
}
