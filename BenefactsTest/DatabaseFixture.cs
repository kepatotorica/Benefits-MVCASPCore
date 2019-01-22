using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Benefacts.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Benefacts.Controllers;
using System.Web.Mvc;
using System;
using DoomedDatabases.Postgres;
using Xunit;


namespace BenefactsTests
{



    //"Host=localhost;Database=cSharp;Username=postgres;Password=4310;Persist Security Info=True"
    public class DatabaseFixture : IDisposable
    {
        private readonly ITestDatabase testDatabase;

        public DatabaseFixture()
        {
            var connectionString = "User ID=postgres;Password=4310;Server=localhost;Database=cSharpTest;";
            testDatabase = new TestDatabaseBuilder().WithConnectionString(connectionString).Build();
            testDatabase.Create();
        }

        public void Dispose()
        {
            testDatabase.Drop();
        }
    }

    [CollectionDefinition("Database")]
    public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
    {
    }
}