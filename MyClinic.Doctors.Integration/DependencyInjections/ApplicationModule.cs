﻿using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

using MyClinic.Doctors.Application.Doctors.Services;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;

namespace MyClinic.Doctors.Integration.DependencyInjections;

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
            cfg.RegisterServicesFromAssemblyContaining<CreateDoctorCommand>();
        });

        return services;
    }

    private static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(CreateDoctorCommandValidator), ServiceLifetime.Transient);
        services.AddFluentValidationAutoValidation();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IDoctorService, DoctorService>();
        //services.AddTransient<IScheduleService, ScheduleService>();
        //services.AddTransient<ISpecialityService, SpecialityService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        //services.AddHostedService<ProcessInterestTransactionsJob>();

        return services;
    }
}