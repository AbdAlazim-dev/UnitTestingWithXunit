using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class CourseTest
    {
        [Fact]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
        {
            //no arrange we only test class construct

            //Acting 
            var course = new Course("Disaster Management 101");

            //Assert
            Assert.True(course.IsNew);
        }
    }
}
