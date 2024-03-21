using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Common.Persistences.ConnectionFactory;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Persistence.Contexts;
using MyClinic.Doctors.Persistence.UnitOfWork;
using MyClinic.Doctors.Persistence.Repositories;

namespace MyClinic.Doctors.Integration.DependencyInjections;

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

        services.AddDbContext<MyClinicDoctorDbContext>((sp, opts) =>
        {
            opts.UseSqlServer(connectionString);
            //.AddInterceptors(
            //    sp.GetRequiredService<PublishDomainEventsToOutBoxMessagesInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        //services.AddSingleton<PublishDomainEventsToOutBoxMessagesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDoctorRepository, DoctorRepository>();
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddTransient<ISpecialityRepository, SpecialityRepository>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //services.AddHostedService<ProcessOutboxMessagesJob>();

        return services;
    }
}