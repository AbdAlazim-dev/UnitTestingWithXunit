
using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Moq;
using System.Security.Claims;

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
        [Fact]
        public void GetProtectedInternalEmployee_GetActionForUseInternalEmployee_MustRedirectToProtectedInternalEmployeeRout()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            var mapper = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController
                (employeeService.Object, mapper.Object);


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Abdalzim"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdinitiy = new ClaimsIdentity(claims, "unitTesting");
            var claimsPrinciple = new ClaimsPrincipal(claimsIdinitiy);

            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrinciple
            };

            demoInternalEmployeeController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act 
            var result = demoInternalEmployeeController.GetProtectedInternalEmployee();

            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("GetInternalEmployees", redirectToActionResult.ActionName);

            Assert.Equal("GetProtectedInternalEmployees", redirectToActionResult.ControllerName);



        }
    }
}
