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
        [Theory]
        [InlineData("How to handle such a thing")]
        [InlineData("The importance of that thing")]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue(string courseTitle)
        {
            //no arrange we only test class construct

            //Acting 
            var course = new Course(courseTitle);

            //Assert
            Assert.True(course.IsNew);
        }
    }
}
