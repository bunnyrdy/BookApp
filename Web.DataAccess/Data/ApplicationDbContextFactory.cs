using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Web.DataAccess.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Create an instance of DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Load the configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set base path to the current directory
                .AddJsonFile("appsettings.json") // Load appsettings.json for the connection string
                .Build();

            // Get the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure the MySQL provider with the connection string and server version
            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 1)) // Set your MySQL version here
            );

            // Return the configured ApplicationDbContext
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
