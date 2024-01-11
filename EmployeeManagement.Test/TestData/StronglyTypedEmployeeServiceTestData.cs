

namespace EmployeeManagement.Test.TestData
{
    public class StronglyTypedEmployeeServiceTestData : TheoryData<int, bool>
    {
        public StronglyTypedEmployeeServiceTestData() 
        {
            Add(100, true);
            Add(300, false);
            Add(200, false);
            Add(500, false);
            Add(600, true);

        }
    }
}
