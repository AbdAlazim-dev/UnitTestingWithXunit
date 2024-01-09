using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void FullNameGetter_InputFirstNameAndLast_FullNameIsConncatination()
        {
            //Arrange 
            var employee = new InternalEmployee("Abdalazim", "Attya", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Abdalsalam";
            employee.LastName = "Mohammed";

            //Assert
            Assert.Equal("Abdalsalam Mohammed", employee.FullName);
        }
        [Fact]
        public void FullNameGetter_InputFirstNameAndLast_FullNameStartWithFirstName()
        {
            //Arrange 
            var employee = new InternalEmployee("Abdalazim", "Attya", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Abdalsalam";
            employee.LastName = "Mohammed";

            //Assert
            Assert.StartsWith(employee.FirstName, employee.FullName);
        }
        [Fact]
        public void FullNameGetter_InputFirstNameAndLast_FullNameEndsWith()
        {
            //Arrange 
            var employee = new InternalEmployee("Abdalazim", "Attya", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Abdalsalam";
            employee.LastName = "Mohammed";

            //Assert
            Assert.EndsWith(employee.LastName, employee.FullName);
        }
        [Fact]
        public void FullNameGetter_InputFirstNameAndLast_FullNameContain()
        {
            //Arrange 
            var employee = new InternalEmployee("Abdalazim", "Attya", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Abdalsalam";
            employee.LastName = "Mohammed";

            //Assert
            Assert.Contains("lam Moh", employee.FullName);
        }
        [Fact]
        public void FullNameGetter_InputFirstNameAndLast_FullNameSoundLike()
        {
            //Arrange 
            var employee = new InternalEmployee("Abdalazim", "Attya", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Abdulsalam";
            employee.LastName = "Mohammad";

            //Assert
            Assert.Matches("Abd(al|ul)salam Mohamm(e|a)d", employee.FullName);
        }
    }
}
