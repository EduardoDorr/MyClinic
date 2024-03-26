using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace MyClinic.Appointments.Integration.DependencyInjections;

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
        //services.AddMediatR(cfg =>
        //{
        //    cfg.RegisterServicesFromAssemblyContaining<CreateAppointmentCommand>();
        //});

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssemblyContaining(typeof(CreateAppointmentCommandValidator), ServiceLifetime.Transient);
        //services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        //services.AddTransient<IAppointmentService, AppointmentService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //services.AddHostedService<ProcessInterestTransactionsJob>();

        return services;
    }
}