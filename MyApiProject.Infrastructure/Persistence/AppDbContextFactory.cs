using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyApiProject.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = FindApiProjectDirectory();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("PostgreSQL")
                ?? throw new InvalidOperationException(
                    "The 'ConnectionStrings:PostgreSQL' setting is required.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                connectionString,
                options => options.MigrationsAssembly("MyApiProject.Infrastructure"));

            return new AppDbContext(optionsBuilder.Options);
        }

        private static string FindApiProjectDirectory()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory is not null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "appsettings.json")))
                    return directory.FullName;

                var apiDirectory = Path.Combine(directory.FullName, "MyApiProject.API");

                if (File.Exists(Path.Combine(apiDirectory, "appsettings.json")))
                    return apiDirectory;

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Could not locate the MyApiProject.API directory containing appsettings.json.");
        }
    }
}
