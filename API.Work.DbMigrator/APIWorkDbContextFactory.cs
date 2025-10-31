using API.Work.EntityFrameWork.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace API.Work.DbMigrator
{
    public class APIWorkDbContextFactory : IDesignTimeDbContextFactory<APIWorkDbContext>
    {
        public APIWorkDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<APIWorkDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("API.Work.EntityFrameWork"));

            return new APIWorkDbContext(optionsBuilder.Options);
        }
    }
}
    