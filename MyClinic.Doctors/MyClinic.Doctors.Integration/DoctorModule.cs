using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Doctors.Integration.DependencyInjections;

namespace MyClinic.Doctors.Integration;

public static class DoctorModule
{
    public static IServiceCollection AddDoctorModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication()
                .AddInfrastructure(configuration);

        return services;
    }
}