using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RepositoryLayer.Context
{
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<AuthenticationContext>
    {
        public AuthenticationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AuthenticationContext>();

            var connectionString = configuration.GetConnectionString("IdentityConnection");

            builder.UseSqlServer(connectionString);

            return new AuthenticationContext(builder.Options);
        }
    }
}
