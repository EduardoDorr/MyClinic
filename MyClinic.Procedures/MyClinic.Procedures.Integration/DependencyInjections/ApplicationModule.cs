using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

using MyClinic.Procedures.Application.Procedures.Services;
using MyClinic.Procedures.Application.Procedures.CreateProcedure;

namespace MyClinic.Procedures.Integration.DependencyInjections;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator()
                .AddValidator()
                .AddServices()
                .AddBackgroundJobs();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateProcedureCommand>();
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(CreateProcedureCommandValidator), ServiceLifetime.Transient);
        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IProcedureService, ProcedureService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //services.AddHostedService<ProcessInterestTransactionsJob>();

        return services;
    }
}