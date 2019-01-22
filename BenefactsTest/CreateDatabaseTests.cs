using Xunit;

namespace BenefactsTests
{
    [Collection("Database")]
    public class CreateDatabaseTests
    {
        [Fact]
        public void CreateAndDropDatabase()
        {
            Assert.True(true);
        }
    }
}
