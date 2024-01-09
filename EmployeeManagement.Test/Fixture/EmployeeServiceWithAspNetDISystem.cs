using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.Fixture
{
    internal class EmployeeServiceWithAspNetDISystem : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        public IEmployeeService EmployeeService 
        { 
            get
            {
                return _serviceProvider.GetService<IEmployeeService>();
            }
        }
        public IEmployeeManagementRepository EmployeeManagementRepository
        {
            get
            {
                return _serviceProvider.GetService<IEmployeeManagementRepository>();
            }
        }
        public EmployeeServiceWithAspNetDISystem() 
        {
            var service = new ServiceCollection();
            service.AddScoped<EmployeeFactory>();
            service.AddScoped<IEmployeeManagementRepository, EmployeeManagementTestDataRepository>();
            service.AddScoped<IEmployeeService, EmployeeService>();

            _serviceProvider = service.BuildServiceProvider();
        }
        public void Dispose()
        {
           //clean up
        }
    }
}
