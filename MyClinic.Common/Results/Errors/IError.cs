namespace MyClinic.Common.Results.Errors;

public interface IError
{
    string Code { get; init; }
    string Message { get; init; }
    public ErrorType Type { get; init; }
}