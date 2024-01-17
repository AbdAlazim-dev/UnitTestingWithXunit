
using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    public class DemoInternalEmployeeTest
    {
        [Fact]
        public async Task CreateInternalEmployee_InvalidInput_MustReturnBadRequest()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            var mapper = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController
                (employeeService.Object, mapper.Object);
            //Creating Model instance
            var internalEmployee = new InternalEmployeeDto();
            // make it invalid
            demoInternalEmployeeController.ModelState.AddModelError("FirstName", "Required");

            // Act 
            var result = await demoInternalEmployeeController
                .CreateInternalEmployee(internalEmployee);
            // Assert 

            var actionResult = 
                Assert.IsType<ActionResult<Models.InternalEmployeeDto>>(result);
            
            //Check if it resolve in BadRequest objectResult

            var badRequestObject = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.IsType<SerializableError>(badRequestObject.Value);
        }
    }
}
