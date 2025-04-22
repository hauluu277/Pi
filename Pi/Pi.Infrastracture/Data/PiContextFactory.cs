using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pi.Infrastracture.Data
{

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<PiContext>

    {

        public PiContext CreateDbContext(string[] args)

        {

            var optionsBuilder = new DbContextOptionsBuilder<PiContext>();

            // Read the connection string from appsettings.json or environment variables

            IConfigurationRoot configuration = new ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json")

                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new PiContext(optionsBuilder.Options);

        }

    }
}
