using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyClinic.Patients.DependencyInjection;

public static class PatientModule
{
    public static IServiceCollection AddPatientModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationModule()
                .AddPersistenceModule(configuration);

        return services;
    }

    private static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddApplication();

        return services;
    }

    private static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
}