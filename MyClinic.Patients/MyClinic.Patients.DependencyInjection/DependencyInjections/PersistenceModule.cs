﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyClinic.Patients.Domain.UnitOfWork;
using MyClinic.Patients.Persistence.Contexts;
using MyClinic.Patients.Persistence.UnitOfWork;
using MyClinic.Patients.Persistence.Repositories;
using MyClinic.Patients.Domain.Repositories;
using MyClinic.Common.Persistence.DbConnectionFactories;

namespace MyClinic.Patients.Integration.DependencyInjections;

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

        services.AddDbContext<MyClinicPatientDbContext>(opts =>
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
        services.AddTransient<IPatientRepository, PatientRepository>();
        services.AddTransient<IInsuranceRepository, InsuranceRepository>();

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