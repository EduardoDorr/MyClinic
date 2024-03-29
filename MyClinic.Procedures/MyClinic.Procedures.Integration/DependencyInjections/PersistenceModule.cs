using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyClinic.Procedures.Domain.UnitOfWork;
using MyClinic.Procedures.Persistence.Contexts;
using MyClinic.Procedures.Persistence.UnitOfWork;
using MyClinic.Procedures.Persistence.Repositories;
using MyClinic.Procedures.Domain.Repositories;
using MyClinic.Common.Persistence.DbConnectionFactories;

namespace MyClinic.Procedures.Integration.DependencyInjections;

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

        services.AddDbContext<MyClinicProcedureDbContext>(opts =>
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
        services.AddTransient<IProcedureRepository, ProcedureRepository>();

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