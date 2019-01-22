using Benefacts.Controllers;
using Benefacts.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Xunit;

namespace BenefactsTests
{
    public class AdminsTest : IClassFixture<DatabaseFixture>
    {
        private ServiceProvider _serviceProvider;

        public AdminsTest(DatabaseFixture fixture)
        {
            //_serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async void Test1()
        {
            using (var context = _serviceProvider.GetService<cSharpContext>())
            {
                var controller = new AdminsController(context); //I don't know if this is going to work
                var actResult = await controller.Index() as ViewResult;
                Assert.Equal("Index", actResult.ViewName);
            }
        }
    }
}
