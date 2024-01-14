

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
        [Fact]
        public async Task GetInternalEmployee_GetAction_ReturnTypeMustBeObjectResult()
        {
            // Arrange 
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeesAsync())
                .ReturnsAsync(new List<InternalEmployee>()
                {
                    new InternalEmployee("Morgan", "Freman", 2, 2500, false, 1),
                    new InternalEmployee("abdula", "Mohammed", 2, 2500, false, 1),
                    new InternalEmployee("Ahmed", "Rashid", 2, 2500, false, 1)
                });
            var internalEmployeeController = new InternalEmployeesController(employeeServiceMock.Object, null);

            // Act 
            var result = await internalEmployeeController.GetInternalEmployees();

            //Assert
            var actionResult = 
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}
