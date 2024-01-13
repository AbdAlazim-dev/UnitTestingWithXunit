
using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.Fixture;
using Moq;

namespace EmployeeManagement.Test
{
    public class MoqTests
    {
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedTheFirstObligotoryCourse_WithObject()
        {
            // Arrage 
            var empolyeeRerpository = new EmployeeManagementTestDataRepository();
            var employeeFactoryMoq = new Mock<EmployeeFactory>();
            //if the first name contain m return mohammed as first name
            employeeFactoryMoq.Setup(m =>
            m.CreateEmployee(It.Is<string>(value => value.Contains("m")),
            "Attya",
            null,
            false)).Returns(new InternalEmployee("Mohammed", "Attya", 5, 2500, false, 1));
            //if the first name contain z return abdalazim as the first name
            employeeFactoryMoq.Setup(m =>
            m.CreateEmployee(It.Is<string>(value => value.Contains("z")),
            "Attya",
            null,
            false)).Returns(new InternalEmployee("Abdalazim", "Attya", 5, 2500, false, 1)); ;

            var employeeService = new EmployeeService(empolyeeRerpository, employeeFactoryMoq.Object);

            //get the first obligotory course 
            var obligotoryCourse = empolyeeRerpository.GetCourse(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            //Act : Create an internal employee
            var internalEmployee = employeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the first obligatory course
            //Assert.Contains(obligotoryCourse, internalEmployee.AttendedCourses);
            Assert.Equal("Abdalazim", internalEmployee.FirstName);
        }
    }
}
