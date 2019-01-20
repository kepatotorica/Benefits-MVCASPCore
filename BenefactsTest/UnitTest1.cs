using System;
using Xunit;
using Benefacts.Controllers;
using Benefacts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace BenefactsTests
{

    public class UnitTest1
    {
        //var context = new DbContextOptionsBuilder<cSharpContext>().UseNpgsql("Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True");//before UseNpgsql I had UseSqlServer and it caused tons of errors, becuase it was the wrong database provider


        [Fact]
        public void HomeAboutTest()
        {
            ////var optionsBuilder = new DbContextOptionsBuilder<cSharpContext>();
            ////optionsBuilder.UseInMemoryDatabase();//THISSS IS NOT POSSIBLE WITH POSTGRESS!!!!!!!!!!!!
            ////var _dbContext = new cSharpContext(optionsBuilder.Options);
            ////var _context = Services

            //var connection = new SqliteConnection("DataSource=:memory:");
            //connection.Open();

            //try
            //{
            //    var options = new DbContextOptionsBuilder<cSharpContext>()
            //        .UseSqlite(connection)
            //        .Options;

            //    // Create the schema in the database
            //    using (var context = new cSharpContext(options))
            //    {
            //        context.Database.EnsureCreated();
            //    }

            //    //// Run the test against one instance of the context
            //    using (var context = new cSharpContext(options))
            //    {
            //        var service = new HomeController(context);
            //        //service.Add("http://sample.com");
            //        service.About();
            //    }

            //    // Use a separate instance of the context to verify correct data was saved to database
            //    using (var context = new cSharpContext(options))
            //    {
            //        var controller = new HomeController(context); //I don't know if this is going to work
            //        var actResult = controller.About() as ViewResult;
            //        Assert.Equal("About", actResult.ViewName);
            //    }
            //}
            //finally
            //{
            //    connection.Close();
            //}

            using (var context = new cSharpContext(DbOptionsFactory.DbContextOptions))
            {
                var controller = new HomeController(context); //I don't know if this is going to work
                var actResult = controller.About() as ViewResult;
                Assert.Equal("About", actResult.ViewName);
            }
            //var controller = new HomeController(new cSharpContext()); //I don't know if this is going to work
            //var controller = new HomeController(serviceMock.Object); //I don't know if this is going to work

        }

        [Fact]
        public void PassTest()
        {
            Assert.Equal(4, 4);
        }

        [Fact]
        public void FailTest()
        {
            Assert.Equal(2, 4);
        }
    }
}
