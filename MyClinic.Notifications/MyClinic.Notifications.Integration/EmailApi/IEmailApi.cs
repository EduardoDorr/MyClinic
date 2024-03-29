using MyClinic.Common.Results;
using MyClinic.Common.IntegrationsEvents;

namespace MyClinic.Notifications.Integration.EmailApi;

public interface IEmailApi
{
    Task<Result> SendEmail(SendEmailEvent email);
}