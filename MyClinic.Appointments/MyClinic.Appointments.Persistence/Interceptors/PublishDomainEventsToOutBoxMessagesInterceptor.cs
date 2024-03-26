using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Newtonsoft.Json;

using MyClinic.Common.Entities;
using MyClinic.Common.Persistence.Outbox;

namespace MyClinic.Appointments.Persistence.Interceptors;

public sealed class PublishDomainEventsToOutBoxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        if (eventData.Context is not null)
            await InsertOutBoxMessagesAsync(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task InsertOutBoxMessagesAsync(DbContext context)
    {
        var outboxMessages = context
            .ChangeTracker
            .Entries<BaseEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    })
            })
            .ToList();

        await context.Set<OutboxMessage>().AddRangeAsync(outboxMessages);
    }
}