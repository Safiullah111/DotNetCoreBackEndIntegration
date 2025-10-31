
using API.Work.Presentation.MiddleWare;

namespace API.Work.Controllers.DependencyInjection.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddControllerModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IExceptionStatusCodeMapper, ExceptionStatusCodeMapper>();

        return services;
    }
}
