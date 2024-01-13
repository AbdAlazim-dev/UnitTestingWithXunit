using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.DbContexts;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.HttpsMassageHandler;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace EmployeeManagement.Test
{
    public class TestIsolationApproachTest
    {
        [Fact]
        public async Task AttenedCourseAsync_CourseAttended_SuggestedBounceMustCorrectlyBeRecalculated()
        {
            // Arrange

            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var optoinBuilder = 
                new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseSqlite(connection);

            var dBContext = new EmployeeDbContext(optoinBuilder.Options);

            dBContext.Database.Migrate();

            var employeeManagementRepository = 
                new EmployeeManagementRepository(dBContext);

            var emplyeeService = new EmployeeService(employeeManagementRepository
                , new EmployeeFactory());

            var internalEmployee = employeeManagementRepository
                .GetInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            var courseToAttend =
                await employeeManagementRepository.
                GetCourseAsync(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));

            if(courseToAttend == null || internalEmployee == null) 
            {
                throw new XunitException("Arranging the test failed");
            }

            var suggestedbounceMustBe = internalEmployee.YearsInService
                * (internalEmployee.AttendedCourses.Count + 1) * 100;

            // Act
            await emplyeeService.AttendCourseAsync(internalEmployee, courseToAttend);
            //Assert
            Assert.Equal(internalEmployee.SuggestedBonus, suggestedbounceMustBe);
        }
        //Creating a test for a service that require calling into an api, by Creating our custom message handler
        [Fact]
        public async Task PromoteInternalEmployeeAsync_IsEligible_EmployeeJobLevelMustBeIncreased()
        {
            //Arrange
            //Arrange : passing our testable messageHandler to the HttpClient
            var httpClient = new HttpClient(
                new TestEmployeeEligibiltyMessageHandler(true));
            //Arrange : Create An Internal Employee
            var internalEmployee = new InternalEmployee("Abdalazim", "Attya", 2, 2500, false, 1);

            var jobLevelBeforePromotion = internalEmployee.JobLevel;
            //Arrange : Create an instance of the class that need to call external Api
            var promotionService = new PromotionService(httpClient, new EmployeeManagementTestDataRepository());

            //Act : use the method that call the internal employee

            await promotionService.PromoteInternalEmployeeAsync(internalEmployee);

            //Assert : is the internal employee job level increaced ?

            Assert.Equal(internalEmployee.JobLevel, jobLevelBeforePromotion + 1);
        }
    }
}
