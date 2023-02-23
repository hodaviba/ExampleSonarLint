using AutoMapper;
using Employees.Api.Dtos;
using Employees.Api.Mapper;
using Employees.Api.Services;
using Employees.Api.Test.Mocks.Context;
using FakeItEasy;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Employees.Api.Test.Services
{
    public class EmployeeServicesTest
    {
        private readonly IMapper mockMapper;

        public EmployeeServicesTest()
        {
            mockMapper = A.Fake<IMapper>();
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new AutoMapping());
            });
            mockMapper = config.CreateMapper();
        }

        [Fact]
        public async Task When_AddEmployee_ShouldTrue()
        {
            //Arrange
            var employee = new EmployeeDto()
            {
                Name = "Marcos",
                Age = 68
            };
            var dbContext = await DbContextFake.GetDatabaseContext();
            var employeeService = new EmployeeServices(dbContext, mockMapper);

            //Act
            var result = await employeeService.AddEmployee(employee);

            //Assert
            result.Should().BeTrue();
            Assert.True(result);
        }

        [Fact]
        public async Task When_GetEmployees_ShouldReturnEmployeesList()
        {
            //Arrange
            var dbContext = await DbContextFake.GetDatabaseContext();
            var employeeService = new EmployeeServices(dbContext, mockMapper);

            //Act
            var result = await employeeService.GetEmployees();

            //Assert
            result.Should().NotBeEmpty().And.HaveCount(2);
        }
    }
}