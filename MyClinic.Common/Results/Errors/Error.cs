namespace MyClinic.Common.Results.Errors;

public sealed record Error(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
}