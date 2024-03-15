using Microsoft.AspNetCore.Mvc;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;

namespace MyClinic.API.Extensions;

public static class ResultExtension
{
    public static IActionResult ToProblemDetails(this IResultBase result)
    {
        if (result.Success)
            throw new InvalidOperationException("Result is a success!");

        var error = result.Errors[0];

        var problemDetails = Results.Problem(
            statusCode: GetStatusCode(error.Type),
            title: GetTitle(error.Type),
            type: GetType(error.Type),
            extensions: new Dictionary<string, object?>
            {
                {"errors", new[] {result.Errors} }
            });

        return (IActionResult)problemDetails;
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Internal Server Error",
        };

    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
        };
}