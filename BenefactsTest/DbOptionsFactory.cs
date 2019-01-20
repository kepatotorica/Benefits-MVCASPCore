using Benefacts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefactsTests
{
    public static class DbOptionsFactory
    {
        static DbOptionsFactory()
        {
            var connectionString = "Host = localhost; Database = cSharp; Username = postgres; Password = 4310; Persist Security Info = True";

            DbContextOptions = new DbContextOptionsBuilder<cSharpContext>()
                .UseNpgsql(connectionString)
                .Options;
        }

        public static DbContextOptions<cSharpContext> DbContextOptions { get; }

    }
}
