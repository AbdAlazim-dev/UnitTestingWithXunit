using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    [Collection("EmplyeeServiceCollection")]
    public class DataDrivenEmployeeServiceTest //: IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _employeeServiceFixture;

        public DataDrivenEmployeeServiceTest(EmployeeServiceFixture employeeServiceFixture) 
        {
            _employeeServiceFixture = employeeServiceFixture;
        }
        [Fact]
        public async Task GiveRise_RiseBellowMinmumGivin_ExceptionMustBeThrown()
        {

            var internalEmployee = new InternalEmployee("Abdalazim", "Attya", 1, 2500, false, 1);

            //Act & Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(async () =>
            {
                await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 5);
            });

        }
        [Fact]
        public void GiveRise_RiseBellowMinmumGivin_ExceptionMustBeThrown_Mistak()
        {

            var internalEmployee = new InternalEmployee("Abdalazim", "Attya", 1, 2500, false, 1);

            //Act & Assert
            //this wont check if this exception is the same as the exception was thrown because we did not await it
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 5);
            });

        }
        [Theory]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedTheObligotoryCourses(Guid courseId)
        {

            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.Contains(internalEmployee.AttendedCourses,
                course => course.Id == courseId);
        }
        public static IEnumerable<object[]> ExampleTestDrivenData_WithProperty 
        { 
            get
            {
                return new List<object[]>
                {
                    new object[] {100, true},
                    new object[] {200, false}

                };
            }
        }
        public static IEnumerable<object[]> ExampleTestDrivenData_WithMethod(
            int testDataInstance)
        {

            var testdata =  new List<object[]>
            {
                new object[] {100, true},
                new object[] {200, false}

            };
            return testdata.Take(testDataInstance);
        }
        [Theory]
        [MemberData(nameof(ExampleTestDrivenData_WithMethod), 1)]
        public async Task GiveRise_RiseGiven_EmployeeMinmumRiseGivenMatchesValue(int riseGiven,
            bool expectedValueFromMinimumRiseGiven)
        {
            // Arrange 
            var internalEmployee = 
                new InternalEmployee("Abdalazim", "Attya", 2, 2500, false, 1);
            // Act
            await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, riseGiven);

            //Assert
            Assert.Equal
                (expectedValueFromMinimumRiseGiven, internalEmployee.MinimumRaiseGiven);
        }
    }
}
