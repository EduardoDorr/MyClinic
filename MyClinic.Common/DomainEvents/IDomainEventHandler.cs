using MediatR;

namespace MyClinic.Common.DomainEvents;

public interface IDomainEventHandler<TDomainEvent>
    : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
}