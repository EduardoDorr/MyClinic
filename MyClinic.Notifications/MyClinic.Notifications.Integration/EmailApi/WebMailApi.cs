using System.Text;
using System.Net.Mime;
using System.Text.Json;

using Microsoft.Extensions.Options;

using MyClinic.Common.Options;
using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Common.IntegrationsEvents;

namespace MyClinic.Notifications.Integration.EmailApi;

public sealed class WebMailApi : IWebMailApi
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WebMailApiOptions _webMailApiOptions;

    public WebMailApi(IHttpClientFactory httpClientFactory, IOptions<WebMailApiOptions> webMailApiOptions)
    {
        _httpClientFactory = httpClientFactory;
        _webMailApiOptions = webMailApiOptions.Value;
    }

    public async Task<Result> SendEmail(SendEmailEvent email)
    {
        using var httpClient = _httpClientFactory.CreateClient(_webMailApiOptions.ApiName);

        var json =
            new StringContent(
                JsonSerializer.Serialize(email),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

        using HttpResponseMessage httpResponse =
            await httpClient.PostAsync(_webMailApiOptions.EmailEndpoint, json);

        if (httpResponse.IsSuccessStatusCode)
            return Result.Ok();

        return Result.Fail(new Error("WebMailApi.Error", await httpResponse.Content.ReadAsStringAsync(), ErrorType.Failure));
    }
}