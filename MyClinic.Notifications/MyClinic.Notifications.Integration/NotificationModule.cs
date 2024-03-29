using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Notifications.Integration.EmailApi;
using MyClinic.Notifications.Integration.Consumers;

namespace MyClinic.Notifications.Integration;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices()
                .AddConsumers()
                .AddHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailApi, WebMailApi>();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<SendEmailEventConsumerService>();

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("WebMailApi", client =>
        {
            client.BaseAddress = new Uri(configuration["WebMailAPI:Url"]);
        });

        return services;
    }
}