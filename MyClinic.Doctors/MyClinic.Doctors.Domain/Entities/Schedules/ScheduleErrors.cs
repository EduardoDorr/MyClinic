using MyClinic.Common.Results.Errors;

namespace MyClinic.Doctors.Domain.Entities.Schedules;

public sealed record ScheduleErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Schedule.CannotBeCreated", "Something went wrong and the Schedule cannot be created", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Schedule.NotFound", "Not found", ErrorType.NotFound);

    public static readonly Error StartDateIsRequired =
        new("Schedule.StartDateIsRequired", "Start date is required", ErrorType.Validation);

    public static readonly Error EndDateIsRequired =
        new("Schedule.EndDateIsRequired", "End date is required", ErrorType.Validation);

    public static readonly Error EndDateMustBeGreaterThanStartDate =
        new("Schedule.EndDateMustBeGreaterThanStartDate", "End date must be greater than start date", ErrorType.Validation);
}