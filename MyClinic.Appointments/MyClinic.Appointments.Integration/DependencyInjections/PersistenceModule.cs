using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Common.Persistence.ConnectionFactories;

using MyClinic.Appointments.Domain.UnitOfWork;
using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Persistence.Contexts;
using MyClinic.Appointments.Persistence.UnitOfWork;
using MyClinic.Appointments.Persistence.Repositories;
using MyClinic.Appointments.Persistence.Interceptors;
using MyClinic.Appointments.Persistence.BackgroundJobs;

namespace MyClinic.Appointments.Integration.DependencyInjections;

public static class PersistenceModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
                .AddInterceptors()
                .AddRepositories()
                .AddUnitOfWork()
                .AddBackgroundJobs();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString();

        services.AddDbContext<MyClinicAppointmentDbContext>((sp, opts) =>
        {
            opts.UseSqlServer(connectionString)
                .AddInterceptors(
                    sp.GetRequiredService<PublishDomainEventsToOutBoxMessagesInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsToOutBoxMessagesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IAppointmentRepository, AppointmentRepository>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}