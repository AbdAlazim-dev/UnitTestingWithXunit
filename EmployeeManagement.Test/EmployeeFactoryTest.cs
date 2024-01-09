using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTest : IDisposable
    {

        private EmployeeFactory _factory;
        public EmployeeFactoryTest() 
        { 
            _factory = new EmployeeFactory();
        }

        public void Dispose()
        {
            //implement if you have unmanaged Dependencies
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBe2500()
        {

            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.Equal(2500, employee.Salary);
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBeBetween2500_3500()
        {
            //Act
            var employee = (InternalEmployee)_factory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "The Salary is not in acceptable range brother");
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
