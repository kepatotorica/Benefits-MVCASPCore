using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Benefacts.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Benefacts.Controllers;
using System.Web.Mvc;

namespace BenefactsTests
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<cSharpContext>(options => options.UseNpgsql("Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True"),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class UnitTest2 : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public UnitTest2(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async System.Threading.Tasks.Task Test1Async()
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
