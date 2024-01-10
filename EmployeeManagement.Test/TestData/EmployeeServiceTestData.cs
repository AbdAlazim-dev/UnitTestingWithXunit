using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.TestData
{
    internal class EmployeeServiceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, true };
            yield return new object[] { 10, false };
            yield return new object[] { 99, false };
            yield return new object[] { 25, false };
            yield return new object[] { 50, false };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
