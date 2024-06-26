﻿using System.Text.Json.Serialization;

using Microsoft.OpenApi.Models;

using Serilog;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

using MyClinic.Common;
using MyClinic.Common.Options;

using MyClinic.API.Middlewares;
using MyClinic.Doctors.Integration;
using MyClinic.Patients.Integration;
using MyClinic.Procedures.Integration;
using MyClinic.Appointments.Integration;
using MyClinic.Notifications.Integration;

namespace MyClinic.API.Configurations;

public static class ServiceConfiguration
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add Serilog as the log provider.
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        builder.Services.ConfigureOptions(builder.Configuration);

        // Add modules
        builder.Services.AddCommonModule(builder.Configuration);
        builder.Services.AddPatientModule(builder.Configuration);
        builder.Services.AddDoctorModule(builder.Configuration);
        builder.Services.AddProcedureModule(builder.Configuration);
        builder.Services.AddAppointmentModule(builder.Configuration);
        builder.Services.AddNotificationModule(builder.Configuration);


        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MyClinic.API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Eduardo Dörr",
                    Email = "edudorr@hotmail.com",
                    Url = new Uri("https://github.com/EduardoDorr")
                }
            });

            s.AddEnumsWithValuesFixFilters();
        });

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseStaticFiles();

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .CreateLogger();
    }

    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WebMailApiOptions>(options => configuration.GetSection(OptionsConstants.WebMailApiSection).Bind(options));
        services.Configure<GoogleCalendarOptions>(options => configuration.GetSection(OptionsConstants.GoogleCalendarSection).Bind(options));
        services.Configure<RabbitMqConfigurationOptions>(options => configuration.GetSection(OptionsConstants.RabbitMQConfigurationSection).Bind(options));
    }
}