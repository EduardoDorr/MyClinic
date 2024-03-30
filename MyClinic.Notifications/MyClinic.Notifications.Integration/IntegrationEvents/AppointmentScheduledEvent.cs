namespace MyClinic.Notifications.Integration.IntegrationEvents;

public sealed record AppointmentScheduledEvent(Guid AppointmentId, string EventId);