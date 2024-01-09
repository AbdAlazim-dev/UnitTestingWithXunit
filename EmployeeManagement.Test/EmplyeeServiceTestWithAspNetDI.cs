using EmployeeManagement.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmplyeeServiceTestWithAspNetDI 
        : IClassFixture<EmployeeServiceWithAspNetDISystem>
    {
        private readonly EmployeeServiceWithAspNetDISystem _employeeServiceFixture;

        public EmplyeeServiceTestWithAspNetDI(EmployeeServiceWithAspNetDISystem employeeSerivceFixture)
        {
            _employeeServiceFixture = employeeSerivceFixture;
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedAllObligotoryCourse_WithObject()
        {
            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");
            var obligotoryCourses = _employeeServiceFixture.EmployeeManagementRepository.GetCourses(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.Equal(obligotoryCourses, internalEmployee.AttendedCourses);
        }
    }
}
