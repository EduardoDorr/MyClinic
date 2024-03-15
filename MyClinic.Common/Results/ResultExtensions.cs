namespace MyClinic.Common.Results;

public static class ResultExtensions
{
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Result, T> onFailure)
    {
        return result.Success ? onSuccess() : onFailure(result);
    }

    public static T Match<T, TValue>(this Result<TValue> result, Func<TValue?, T> onSuccess, Func<Result<TValue>, T> onFailure)
    {
        return result.Success ? onSuccess(result.ValueOrDefault) : onFailure(result);
    }    
}