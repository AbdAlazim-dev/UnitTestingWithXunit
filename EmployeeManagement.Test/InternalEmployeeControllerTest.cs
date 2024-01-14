

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

        public InternalEmployeeControllerTest() 
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
            _internalEmployeeController = new
                InternalEmployeesController(employeeServiceMock.Object, null);
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
    }
}
