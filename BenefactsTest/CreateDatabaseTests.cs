using Benefacts.Controllers;
using Benefacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

//https://ufukhaciogullari.com/blog/postgresql-integration-testing-in-net-core/ maybe this can help? I don't see him testing controllers though

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

        //private cSharpContext testDbContext;

        //public void UserTests(DatabaseFixture databaseFixture)
        //{
        //    testDbContext = databaseFixture.DbContext;
        //}

        [Fact]
        public async Task InsertUsers()
        {
            DatabaseFixture df = new DatabaseFixture();
            var controller = new AdminsController(df.DbContext);
            var actResult = await controller.Index() as ViewResult;
            Assert.Equal("Index", actResult.ViewName);

            //     public int UId { get; set; }
            //public string FName { get; set; }
            //public string LName { get; set; }
            //public string Email { get; set; }
            //public string Gender { get; set; }
            //await testDbContext.Users.AddAsync(new User { attributes: "values" });
            //await testDbContext.SaveChangesAsync();
            //var count = await testDbContext.Users.CountAsync();
            //Assert.Equal(2, count);
        }
    }
}
