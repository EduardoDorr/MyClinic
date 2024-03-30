namespace MyClinic.Notifications.Integration.GoogleCalendar;

public sealed record GoogleCalendarEventRequest(
    string Summary,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    IList<string> Attendees);