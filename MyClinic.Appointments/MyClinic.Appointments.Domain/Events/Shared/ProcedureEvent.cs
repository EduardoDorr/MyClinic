namespace MyClinic.Appointments.Domain.Events.Shared;

public sealed record ProcedureEvent(string Name, DateTime StartDate);