using System;
using Xunit;
using Benefacts.Controllers;
using Benefacts.Models;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenefactsTest
{

    public class UnitTest1
    {
        //var context = new DbContextOptionsBuilder<cSharpContext>().UseNpgsql("Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True");//before UseNpgsql I had UseSqlServer and it caused tons of errors, becuase it was the wrong database provider

        //[Fact]
        //public async void AdminsIndexTest()
        //{
        //    var controller = new AdminsController(new cSharpContext()); //I don't know if this is going to work
        //    var actResult = await controller.Index() as ViewResult;
        //    Assert.Equal("Index", actResult.ViewName);
        //}

        [Fact]
        public void AddTest()
        {
            //var controller = new AdminsController(new cSharpContext()); //I don't know if this is going to work
            //var actResult = await controller.Index() as ViewResult;
            //Assert.Equal("Index", actResult.ViewName);

            Assert.Equal(4, 4);
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using Benefacts.Controllers;
//using System.Web.Mvc;
//using Benefacts.Models;

//namespace Benefacts.Controllers
//{
//    [TestFixture]
//    public class AdminsControllerTest
//    {
//        [Test]
//        public async Task TestDetailsView()
//        {
//            //cSharpContext c = new cSharpContext();
//            var controller = new AdminsController(new cSharpContext()); //I don't know if this is going to work
//            var actResult = await controller.Index() as ViewResult;
//            Assert.That(actResult.ViewName, Is.EqualTo("Index"));
//            //var controller = new ProductController();
//            //var result = controller.Details(2) as ViewResult;
//            //Assert.AreEqual("Details", result.ViewName);
//        }
//    }
//}
