

using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.InteropServices;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTest
    {
        private readonly InternalEmployeesController _internalEmployeeController;
        private readonly InternalEmployee _firstEmployee;
        public InternalEmployeeControllerTest() 
        {
            _firstEmployee = new InternalEmployee("Morgan", "Freman", 2, 2500, false, 1);
            // Arrange 
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeesAsync())
                .ReturnsAsync(new List<InternalEmployee>()
                {
                    _firstEmployee,
                    new InternalEmployee("abdula", "Mohammed", 2, 2500, false, 1),
                    new InternalEmployee("Ahmed", "Rashid", 2, 2500, false, 1)
                });

            //var mapperMock = new Mock<IMapper>();
            //mapperMock.Setup(m =>
            //m.Map<InternalEmployee, Models.InternalEmployeeDto>
            //(It.IsAny<InternalEmployee>()));

            var mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MapperProfiles.EmployeeProfile>());

            var mapper = new Mapper(mapperConfiguration);

            _internalEmployeeController = new
                InternalEmployeesController(employeeServiceMock.Object, mapper);
        }
        [Fact]
        public async Task GetInternalEmployee_GetAction_ReturnTypeMustBeObjectResult()
        {
            

            // Act 
            var result = await _internalEmployeeController.GetInternalEmployees();

            //Assert
            var actionRestult = 
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);
            
            //result here is the type of the result we will get and 
            //actionResult drived from ObjectResult so it will be true
            Assert.IsType<OkObjectResult>(actionRestult.Result);
        }
        [Fact]
        public async Task GetInternalEmployess_GetAction_ResultTypeMustBeIEnumerableOfInternalEmployeeDtoAsModelType()
        {
            
            // Act 
            var result = await _internalEmployeeController.GetInternalEmployees();

            //Assert 
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(
                ((OkObjectResult)actionResult.Result).Value);
        }
        [Fact]
        public async Task GetInternalEmployess_GetAction_IEnumerableOfInternalEmployeeDtoMustBe3()
        {

            // Act 
            var result = await _internalEmployeeController.GetInternalEmployees();

            //Assert 
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var dto = Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(okObjectResult.Value);

            var firstEmployee = dto.First();
            Assert.Equal(_firstEmployee.FullName, firstEmployee.FullName);
            Assert.Equal(_firstEmployee.FirstName, firstEmployee.FirstName);
            Assert.Equal(_firstEmployee.LastName, firstEmployee.LastName);
            Assert.Equal(_firstEmployee.Salary, firstEmployee.Salary);
            Assert.Equal(_firstEmployee.Id, firstEmployee.Id);
        }
    }
}
