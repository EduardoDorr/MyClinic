using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;

using MyClinic.Appointments.Domain.Events;
using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Persistence.Contexts;

namespace MyClinic.Appointments.Persistence.BackgroundJobs;

public sealed class AppointmentDayBeforeNotificationJob : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<AppointmentDayBeforeNotificationJob> _logger;

    public AppointmentDayBeforeNotificationJob(IServiceProvider provider, ILogger<AppointmentDayBeforeNotificationJob> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<MyClinicAppointmentDbContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

            var appointments = await context
                .Set<Appointment>()
                .Where(a => a.ScheduledStartDate.Date.AddDays(-1) == DateTime.Today)
                .ToListAsync(stoppingToken);

            foreach (var appointment in appointments)
            {
                try
                {
                    var appointmentNotificationEvent =
                        new AppointmentNotificationEvent(appointment.Id);

                    await publisher.Publish(appointmentNotificationEvent, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            if (appointments.Count > 0)
                await context.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}