using Microsoft.Extensions.Options;

using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

using MyClinic.Common.Options;

namespace MyClinic.Notifications.Integration.GoogleCalendar;

public sealed class GoogleCalendarService : IGoogleCalendarService
{
    private const string APPLICATION_NAME = "MyClinic";

    private readonly GoogleCalendarOptions _googleCalendarOptions;

    public GoogleCalendarService(IOptions<GoogleCalendarOptions> googleCalendarOptions)
    {
        _googleCalendarOptions = googleCalendarOptions.Value;
    }

    public async Task<IList<Event>> GetAllEvents()
    {
        string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{_googleCalendarOptions.CalendarId}/events" };

        var services = await ConnectGoogleCalendar(scopes);

        var events = services.Events.List(_googleCalendarOptions.CalendarId).Execute();

        return events.Items;
    }

    public async Task<Event> GetEventById(string eventId)
    {
        string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{_googleCalendarOptions.CalendarId}/events" };

        var services = await ConnectGoogleCalendar(scopes);

        var @event = await services.Events.Get(_googleCalendarOptions.CalendarId, eventId).ExecuteAsync();

        return @event;
    }

    public async Task<Event> CreateEvent(GoogleCalendarEventRequest eventRequest)
    {
        string[] scopes = { "https://www.googleapis.com/auth/calendar " };

        var services = await ConnectGoogleCalendar(scopes);

        var @event = new Event
        {
            Description = eventRequest.Description,
            Summary = eventRequest.Summary,
            Start = GetEventDateTime(eventRequest.StartDate),
            End = GetEventDateTime(eventRequest.EndDate),
            GuestsCanInviteOthers = false,
            Attendees = GetAttendees(eventRequest.Attendees)
        };

        var eventCreated = await services.Events.Insert(@event, _googleCalendarOptions.CalendarId).ExecuteAsync();

        return eventCreated;
    }

    public async Task<Event> UpdateEvent(Event eventCalendar, DateTime startDate, DateTime endDate)
    {
        string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{_googleCalendarOptions.CalendarId}/events/{eventCalendar.Id}" };

        var services = await ConnectGoogleCalendar(scopes);

        eventCalendar.Start.DateTimeDateTimeOffset = startDate;
        eventCalendar.End.DateTimeDateTimeOffset = endDate;

        var events = await services.Events.Update(eventCalendar, _googleCalendarOptions.CalendarId, eventCalendar.Id).ExecuteAsync();

        return events;
    }

    public async Task<string> DeleteEvent(string eventId)
    {
        string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{_googleCalendarOptions.CalendarId}/events/{eventId}" };

        var services = await ConnectGoogleCalendar(scopes);

        var events = await services.Events.Delete(_googleCalendarOptions.CalendarId, eventId).ExecuteAsync();

        return events;
    }

    private static async Task<CalendarService> ConnectGoogleCalendar(string[] scopes)
    {
        UserCredential credential;
        //GoogleCredential credential;

        var fileFullPath = Path.Combine(Directory.GetCurrentDirectory(), "Credential", "credential.json");

        using (var stream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
        {
            var credPath = "token.json";

            //credential =
            //    (await GoogleCredential.FromStreamAsync(stream, CancellationToken.None))
            //    .CreateScoped(scopes);

            credential = await GoogleWebAuthorizationBroker
                .AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
            );
        }

        var services = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = APPLICATION_NAME
        });

        return services;
    }

    private static EventDateTime GetEventDateTime(DateTime dateTime)
    {
        var eventDateTime =
            new EventDateTime
            {
                DateTimeDateTimeOffset = dateTime,
                TimeZone = "America/Sao_Paulo"
            };

        return eventDateTime;
    }

    private static List<EventAttendee> GetAttendees(IList<string> attendees)
    {
        var eventAttendees = new List<EventAttendee>();

        foreach (var attendee in attendees)
            eventAttendees.Add(new EventAttendee { Email = attendee, ResponseStatus = "needsAction" });

        return eventAttendees;
    }
}