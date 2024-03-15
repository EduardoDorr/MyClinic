using System.Text.RegularExpressions;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;

namespace MyClinic.Common.ValueObjects;

public sealed partial record Telephone
{
    public string Number { get; }

    private const string _pattern = @"^[0-9]{10,11}$";

    private Telephone() { }

    private Telephone(string number)
    {
        Number = number;
    }

    public static Result<Telephone> Create(string number)
    {
        number = FormatInput(number);

        if (string.IsNullOrWhiteSpace(number))
            return Result.Fail<Telephone>(TelephoneErrors.TelephoneIsRequired);

        if (!IsTelephone(number))
            return Result.Fail<Telephone>(TelephoneErrors.TelephoneIsInvalidFormat);

        var telephone = new Telephone(number);

        return Result.Ok(telephone);
    }

    private static string FormatInput(string number)
    {
        return number.Trim()
                     .Replace("(", "")
                     .Replace(")", "")
                     .Replace("-", "")
                     .Replace(" ", "");
    }

    private static bool IsTelephone(string number) =>
        Regex.IsMatch(number, _pattern);
}

public sealed record TelephoneErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error TelephoneIsRequired =
        new("Telephone.TelephoneIsRequired", "Telephone number is required", ErrorType.Validation);

    public static readonly Error TelephoneIsInvalidFormat =
        new("Telephone.TelephoneIsInvalidFormat", "Telephone number format is invalid", ErrorType.Validation);
}