using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Appointments.Integration.DependencyInjections;

namespace MyClinic.Appointments.Integration;

public static class AppointmentModule
{
    public static IServiceCollection AddAppointmentModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication()
                .AddInfrastructure(configuration);

        return services;
    }
}