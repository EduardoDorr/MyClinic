using System.Text;
using System.Net.Mime;
using System.Text.Json;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Common.IntegrationsEvents;

namespace MyClinic.Notifications.Integration.EmailApi;

public sealed class WebMailApi : IEmailApi
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WebMailApi(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result> SendEmail(SendEmailEvent email)
    {
        using var httpClient = _httpClientFactory.CreateClient("WebMailApi");

        var json =
            new StringContent(
                JsonSerializer.Serialize(email),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

        using HttpResponseMessage httpResponse =
            await httpClient.PostAsync("email", json);

        if (httpResponse.IsSuccessStatusCode)
            return Result.Ok();

        return Result.Fail(new Error("WebMailApi.Error", await httpResponse.Content.ReadAsStringAsync(), ErrorType.Failure));
    }
}