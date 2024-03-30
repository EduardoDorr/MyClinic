using MyClinic.Common.Results;
using MyClinic.Common.IntegrationsEvents;

namespace MyClinic.Notifications.Integration.EmailApi;

public interface IWebMailApi
{
    Task<Result> SendEmail(SendEmailEvent email);
}