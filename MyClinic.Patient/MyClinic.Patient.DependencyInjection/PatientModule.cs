using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyClinic.Patients.DependencyInjection;

public static class PatientModule
{
    public static IServiceCollection AddPatientModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication()
                .AddInfrastructure(configuration);

        return services;
    }
}