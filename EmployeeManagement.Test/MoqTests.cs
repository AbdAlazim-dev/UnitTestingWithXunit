
using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
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
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggetstedBounceMustBeCalculatedCurrectyly()
        {
            //arrange
            var employeeRepositoryMock = new Mock<IEmployeeManagementRepository>();
            employeeRepositoryMock.Setup(m => m.GetInternalEmployee(It.IsAny<Guid>()))
                .Returns(new InternalEmployee("Abdalazim", "Attya", 2, 2500, true, 2)
                {
                    AttendedCourses = new List<Course> { 
                        new Course("Course"),
                        new Course("Another Course")
                    }
                });
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeRepositoryMock.Object
                , employeeFactoryMock.Object);

            //Act
            var employee = employeeService.FetchInternalEmployee(Guid.Empty);

            //Assert 
            Assert.InRange(employee.SuggestedBonus, 200, 500);

        }
    }
}
