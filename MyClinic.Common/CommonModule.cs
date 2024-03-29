using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Common.MessageBus;

namespace MyClinic.Common;

public static class CommonModule
{
    public static IServiceCollection AddCommonModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageBusProducerService, MessageBusProducerService>();

        return services;
    }
}