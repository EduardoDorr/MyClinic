using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Procedures.Integration.DependencyInjections;

namespace MyClinic.Procedures.Integration;

public static class ProcedureModule
{
    public static IServiceCollection AddProcedureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication()
                .AddInfrastructure(configuration);

        return services;
    }
}