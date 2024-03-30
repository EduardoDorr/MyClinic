using Google.Apis.Calendar.v3.Data;

namespace MyClinic.Notifications.Integration.GoogleCalendar;

public interface IGoogleCalendarService
{
    Task<IList<Event>> GetAllEvents();
    Task<Event> GetEventById(string eventId);
    Task<Event> CreateEvent(GoogleCalendarEventRequest eventRequest);
    Task<Event> UpdateEvent(Event eventCalendar, DateTime startDate, DateTime endDate);
    Task<string> DeleteEvent(string eventId);
}