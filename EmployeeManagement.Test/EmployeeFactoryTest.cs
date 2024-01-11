using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTest : IDisposable
    {

        private EmployeeFactory _factory;
        private ITestOutputHelper _output;
        public EmployeeFactoryTest(ITestOutputHelper output) 
        { 
            _factory = new EmployeeFactory();
            _output = output;
        }

        public void Dispose()
        {
            //implement if you have unmanaged Dependencies
        }
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new List<object[]>
                {
                    new object[]  { "abdalazim", "attya" },
                    new object[]  { "abdalasalm", "Mohammed" },
                    new object[]  { "wow", "Samah" }
                };
                
            }
        }
        public static IEnumerable<object[]> TestDataMethod(int howManyTest)
        {
            var testData =  new List<object[]>
            {
                new object[]  { "Mohammed","abdalazim", 2500, 3500 },
                new object[]  { "Khalid", "abdalazim", 1500, 3000 },
                new object[]  { "Rashid", "abdalazim", 500, 3500 }
            };
            return testData.Take(howManyTest);
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBe2500(string firstName,
            string lastName)
        {

            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee(firstName, lastName);

            //Assert
            Assert.Equal(2500, employee.Salary);
        }
        [Theory]
        [MemberData(nameof(TestDataMethod), 3)]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBeBetween2500_3500(
            string firstName, string lastName ,int minimumSalary, int maximumSalary)
        {
            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee(firstName, lastName);

            //Assert
            _output.WriteLine($"The employee {firstName} and his salary around {minimumSalary} - {maximumSalary}");
            Assert.True(employee.Salary >= minimumSalary && employee.Salary <= maximumSalary, "The Salary is not in acceptable range brother");
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustInRange2500_3500()
        {

            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.InRange(employee.Salary, 2500 ,3500);
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_InPrecsion()
        {
            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee("Abdalazim", "Attya");
            employee.Salary = 2500.003m;
            //Assert
            Assert.Equal(employee.Salary, 2500, 2);
        }
        [Fact]
        public void CreateAnEmployee_IsExternalIsTrue_ReturnTypeMustBeExtrenalEmployee()
        {

            //Act
            var employee = _factory.CreateEmployee("Abdalazim", "Attya", "Azimov", true);

            //Assert 
            Assert.IsType<ExternalEmployee>(employee);
        }
        [Fact]
        public void CreateAnEmployee_IsExternalIsTrue_ReturnTypeMustBeEmployee()
        {
            //Act
            var employee = _factory.CreateEmployee("Abdalazim", "Attya", "Azimov", true);

            //Assert 
            Assert.IsAssignableFrom<Employee>(employee);
        }

    }
}
