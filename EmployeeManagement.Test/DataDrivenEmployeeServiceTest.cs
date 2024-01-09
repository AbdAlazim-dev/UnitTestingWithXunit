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
    }
}
