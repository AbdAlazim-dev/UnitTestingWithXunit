using EmployeeManagement.Business;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.Fixture
{
    public class EmployeeServiceFixture : IDisposable
    {
        public EmployeeManagementTestDataRepository EmployeeTestDataRepository { get; }
        public EmployeeService EmployeeService { get; }

        public EmployeeServiceFixture() 
        {
            EmployeeTestDataRepository = new EmployeeManagementTestDataRepository();
            EmployeeService = new EmployeeService(EmployeeTestDataRepository, new EmployeeFactory());
        }

        public void Dispose()
        {
            //clean up
        }
    }
}
