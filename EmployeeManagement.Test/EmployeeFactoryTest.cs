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
    public class EmployeeFactoryTest
    {
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBe2500()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.Equal(2500, employee.Salary);
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustBeBetween2500_3500()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "The Salary is not in acceptable range brother");
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_SalaryMustInRange2500_3500()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Abdalazim", "Attya");

            //Assert
            Assert.InRange(employee.Salary, 2500 ,3500);
        }
        [Fact]
        public void CreateEmployee_CunstructInternalEmployee_InPrecsion()
        {
            //Arrange
            var employeeFactory = new EmployeeFactory();

            //Act
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Abdalazim", "Attya");
            employee.Salary = 2500.003m;
            //Assert
            Assert.Equal(employee.Salary, 2500, 2);
        }
    }
}
