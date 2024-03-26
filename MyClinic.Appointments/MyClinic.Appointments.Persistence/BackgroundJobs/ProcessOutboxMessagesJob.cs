using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using MediatR;
using Newtonsoft.Json;

using MyClinic.Common.Events;
using MyClinic.Common.Persistence.Outbox;

using MyClinic.Appointments.Persistence.Contexts;

namespace MyClinic.Appointments.Persistence.BackgroundJobs;

public sealed class ProcessOutboxMessagesJob : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(IServiceProvider provider, ILogger<ProcessOutboxMessagesJob> logger)
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

            var outboxMessages = await context
                .Set<OutboxMessage>()
                .Where(om => om.ProcessedAt == null)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var outboxMessage in outboxMessages)
            {
                var domainEvent =
                    JsonConvert.DeserializeObject<IDomainEvent>(
                        outboxMessage.Content,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                        });

                if (domainEvent is null)
                {
                    _logger.LogError($"Domain event {outboxMessage.Type} cannot deserialize");

                    continue;
                }

                await publisher.Publish(domainEvent, stoppingToken);

                outboxMessage.ProcessedAt = DateTime.Now;
            }

            await context.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }
}