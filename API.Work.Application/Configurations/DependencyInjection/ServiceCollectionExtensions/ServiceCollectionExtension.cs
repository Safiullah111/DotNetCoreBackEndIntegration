using API.Work.Application.Commands.Users;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using API.Work.Application.Contract.Services.Permissions;
using API.Work.Application.Contract.Services.Roles;
using API.Work.Application.Contract.Services.Users;
using API.Work.Application.Services.Authenticatioin;
using API.Work.Application.Services.JwtSettings;
using API.Work.Application.Services.Permissions;
using API.Work.Application.Services.Roles;
using API.Work.Application.Services.Users;
using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using API.Work.Application.Common.Mapping;


namespace API.Work.Application.Services.Configurations.DependencyInjection.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration
        )
    {

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MapperProfile>();
        });

        var assemblies = DependencyContext.Default.RuntimeLibraries
            .Where(lib => lib.Name.StartsWith("API.Work.Application"))
            .Select(lib => Assembly.Load(new AssemblyName(lib.Name)))
            .ToArray();

        services.AddMediatR(cfg =>
        {
            foreach (var assembly in assemblies)
                cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IUserAppServices, UserAppService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ILoginAppService, LoginAppService>();
        services.AddScoped<IRoleAppService, RoleAppService>();
        services.AddScoped<IPermissionAppService, PermissionAppService>();


        services.AddScoped<UserManager>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
        });
        services.Configure<JwtSetting>(configuration.GetSection("JwtSetting"));

        return services;
    }
}
