using API.Work.EntityFrameWork;
using API.Work.EntityFrameWork.Configurations;
using API.Work.EntityFrameWork.Configurations.DependencyInjection.ServiceCollectionExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace API.Work.DbMigrator;

public class Program
{

    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddEntityFrameWorkModule(context.Configuration);
           
            })
            .Build();

        // Run migrations on startup
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<APIWorkDbContext>();
            try
            {
                if (db == null)
                {
                    Console.WriteLine("Database context is null!");
                }
                else
                {
                    Console.WriteLine("Applying Migrations.......");
                    await db.Database.MigrateAsync();
                    Console.WriteLine("Migrations applied successfully.");
                    Console.WriteLine("Seeding Start.......");
                    await DataSeeder.SeedAsync(db);
                    Console.WriteLine("Seeding completed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while migrating or seeding the database: {ex.Message}");
            }

        }
        await host.RunAsync();
    }
}
