namespace MyClinic.Notifications.Integration.IntegrationEvents.Shared;

public sealed record ProcedureEvent(string Name, DateTime StartDate, DateTime EndDate);