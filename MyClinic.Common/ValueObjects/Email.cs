using System.Text.RegularExpressions;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;

namespace MyClinic.Common.ValueObjects;

public sealed record Email
{
    public string Address { get; } = string.Empty;

    private const string _pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    private Email() { }

    private Email(string address)
    {
        Address = address;
    }

    public static Result<Email> Create(string address)
    {
        if (string.IsNullOrEmpty(address))
            return Result.Fail<Email>(EmailErrors.EmailIsRequired);

        var addressTrimmed = address.ToLower().Trim();

        if (addressTrimmed.Length < 5)
            return Result.Fail<Email>(EmailErrors.EmailIsTooShort);

        if (addressTrimmed.Length > 255)
            return Result.Fail<Email>(EmailErrors.EmailIsTooLong);

        if (!Regex.IsMatch(addressTrimmed, _pattern))
            return Result.Fail<Email>(EmailErrors.EmailIsInvalidFormat);

        var email = new Email(addressTrimmed);

        return Result.Ok(email);
    }

    public override string ToString()
    {
        return Address;
    }
}

public sealed record EmailErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error EmailIsRequired =
        new("Email.EmailIsRequired", "Email address is required", ErrorType.Validation);

    public static readonly Error EmailIsTooShort =
        new("Email.EmailIsTooShort", "Email address is too short", ErrorType.Validation);

    public static readonly Error EmailIsTooLong =
        new("Email.EmailIsTooLong", "Email address is too long", ErrorType.Validation);

    public static readonly Error EmailIsInvalidFormat =
        new("Email.EmailIsInvalidFormat", "Email address format is invalid", ErrorType.Validation);
}