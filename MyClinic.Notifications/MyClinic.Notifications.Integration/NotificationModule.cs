using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Common.Options;

using MyClinic.Notifications.Integration.EmailApi;
using MyClinic.Notifications.Integration.Consumers;
using MyClinic.Notifications.Integration.GoogleCalendar;

namespace MyClinic.Notifications.Integration;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices()
                .AddConsumers()
                .AddHttpClients();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IWebMailApi, WebMailApi>();
        services.AddTransient<IGoogleCalendarService, GoogleCalendarService>();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<SendEmailEventConsumerService>();
        services.AddHostedService<AppointmentCreatedEventConsumerService>();

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        var webMailApiOptions = services.BuildServiceProvider().GetRequiredService<IOptions<WebMailApiOptions>>().Value;

        services.AddHttpClient(webMailApiOptions.ApiName, client =>
        {
            client.BaseAddress = new Uri(webMailApiOptions.BaseUrl);
        });

        return services;
    }
}