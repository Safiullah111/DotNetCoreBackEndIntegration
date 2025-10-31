using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using API.Work.EntityFrameWork.Configurations;
using API.Work.EntityFrameWork.Configurations.Base;
using API.Work.EntityFrameWork.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace API.Work.EntityFrameWork.Configurations.DependencyInjection.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEntityFrameWorkModule(this IServiceCollection services, IConfiguration configuration)
    {
        var useSqliteString = configuration["UseSqlite"];
        bool useSqlite = false;
        if (!string.IsNullOrEmpty(useSqliteString))
        {
            bool.TryParse(useSqliteString, out useSqlite);
        }

        if (useSqlite)
        {
            var sqliteConn = configuration.GetConnectionString("SqliteConnection") ?? "Data Source=mydatabase.db";
            services.AddDbContext<APIWorkDbContext>(options =>
                options.UseSqlite(sqliteConn));
        }
        else
        {

            services.AddDbContext<APIWorkDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("API.Work.EntityFrameWork"); // ✅ Tell EF where to put migrations
                    });
                options.ConfigureWarnings(w =>
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            });
        }
        services.AddScoped(typeof(IRepositoryBase<Permission>), typeof(RepositoryBase<Permission, APIWorkDbContext>));
        services.AddScoped(typeof(IRepositoryBase<Role>), typeof(RepositoryBase<Role, APIWorkDbContext>));
        services.AddScoped(typeof(IRepositoryBase<RefreshToken>), typeof(RepositoryBase<RefreshToken, APIWorkDbContext>));
        services.AddScoped(typeof(IRepositoryBase<UserLogin>), typeof(RepositoryBase<UserLogin, APIWorkDbContext>));
        services.AddScoped(typeof(IRepositoryBase<LoginFailure>), typeof(RepositoryBase<LoginFailure, APIWorkDbContext>));

        services.AddScoped<IUserRepository, EfCoreUserRepository>();

        services.AddScoped(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));


        return services;
    }
}
