using MyClinic.Common.DomainEvents;

namespace MyClinic.Appointments.Domain.Events;

public sealed record AppointmentNotificationEvent(Guid AppointmentId) : IDomainEvent;