using System;
using Xunit;
using Benefacts.Controllers;
using Benefacts.Models;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BenefactsTests
{

    public class UnitTest1
    {
        //var context = new DbContextOptionsBuilder<cSharpContext>().UseNpgsql("Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True");//before UseNpgsql I had UseSqlServer and it caused tons of errors, becuase it was the wrong database provider


        [Fact]
        public void HomeAboutTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<cSharpContext>();
            optionsBuilder.UseInMemoryDatabase();
            var _dbContext = new cSharpContext(optionsBuilder.Options);



            //var controller = new HomeController(new cSharpContext()); //I don't know if this is going to work
            //var controller = new HomeController(serviceMock.Object); //I don't know if this is going to work
            var controller = new HomeController(_dbContext); //I don't know if this is going to work
            var actResult = controller.About() as ViewResult;
            Assert.Equal("Index", actResult.ViewName);
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
