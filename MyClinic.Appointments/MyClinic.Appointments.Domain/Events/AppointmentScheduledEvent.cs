using MyClinic.Common.DomainEvents;

namespace MyClinic.Appointments.Domain.Events;

public sealed record AppointmentScheduledEvent(Guid AppointmentId, string EventId) : IDomainEvent;