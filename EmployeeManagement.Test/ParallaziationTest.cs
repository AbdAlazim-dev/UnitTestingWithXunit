
namespace EmployeeManagement.Test
{
    [CollectionDefinition("the parallam", DisableParallelization = true)]
    public class ParallizationTest2
    {

        [Fact]
        public void Test()
        {
            Thread.Sleep(2000);
            Assert.True(true);
        }
        [Fact]
        public void Test2()
        {
            Thread.Sleep(2000);
            Assert.True(true);
        }
        [Fact]
        public void Test3()
        {
            Thread.Sleep(2000);
            Assert.True(true);
        }
    }
}
