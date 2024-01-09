﻿using EmployeeManagement.Business;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    [Collection("EmplyeeServiceCollection")]
    public class EmployeeServiceTest //: IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _employeeServiceFixture;
        public EmployeeServiceTest(EmployeeServiceFixture employeeServiceFixture) 
        {
            _employeeServiceFixture = employeeServiceFixture;
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedTheFirstObligotoryCourse_WithObject()
        {

            //get the first obligotory course 
            var obligotoryCourse = _employeeServiceFixture.EmployeeTestDataRepository.GetCourse(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the first obligatory course
            Assert.Contains(obligotoryCourse, internalEmployee.AttendedCourses);
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedTheFirstObligotoryCourse_WithPredict()
        {

            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the first obligatory course
            Assert.Contains(internalEmployee.AttendedCourses, 
                course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedTheSecondObligotoryCourse_WithPredict()
        {

            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.Contains(internalEmployee.AttendedCourses,
                course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustBeAttendedAllObligotoryCourse_WithObject()
        {
            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");
            var obligotoryCourses = _employeeServiceFixture.EmployeeTestDataRepository.GetCourses(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.Equal(obligotoryCourses ,internalEmployee.AttendedCourses);
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {

            //Act : Create an internal employee
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.All(internalEmployee.AttendedCourses,
                course =>  Assert.False(course.IsNew));
        }
        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_ObligatoryCourseMustBeAttendedCourses()
        {
            //Arrange : get instance of the employee service and the course we want to check
            var obligotoryCourses = await _employeeServiceFixture.EmployeeTestDataRepository.GetCoursesAsync(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
            //Act : Create an internal employee
            var internalEmployee = await _employeeServiceFixture
                .EmployeeService.CreateInternalEmployeeAsync("Abdalazim", "Attya");

            //Assert : Check if the internal employee attended the second obligatory course
            Assert.Equal(obligotoryCourses, internalEmployee.AttendedCourses);
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
    }
}
