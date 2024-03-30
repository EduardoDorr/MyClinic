using MyClinic.Common.DomainEvents;

using MyClinic.Notifications.Integration.IntegrationEvents.Shared;

namespace MyClinic.Notifications.Integration.IntegrationEvents;

public sealed record AppointmentCreatedEvent(
    Guid AppointmentId,
    PersonEvent Patient,
    PersonEvent Doctor,
    ProcedureEvent Procedure) : IDomainEvent;