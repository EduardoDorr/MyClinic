using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Patients.Integration.DependencyInjections;

namespace MyClinic.Patients.Integration;

public static class PatientModule
{
    public static IServiceCollection AddPatientModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication()
                .AddInfrastructure(configuration);

        return services;
    }
}