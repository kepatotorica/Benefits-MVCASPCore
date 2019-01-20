using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Benefacts.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

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
}
