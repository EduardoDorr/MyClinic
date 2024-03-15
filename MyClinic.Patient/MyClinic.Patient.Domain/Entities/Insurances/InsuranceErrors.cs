using MyClinic.Common.Results.Errors;

namespace MyClinic.Patients.Domain.Entities.Insurances;

public sealed record InsuranceErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Insurance.CannotBeCreated", "Something went wrong and the Insurance cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Insurance.CannotBeUpdated", "Something went wrong and the Insurance cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Insurance.CannotBeDeleted", "Something went wrong and the Insurance cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Insurance.NotFound", "Not found", ErrorType.NotFound);

    public static readonly Error NameIsRequired =
        new("Insurance.NameIsRequired", "Name is required", ErrorType.Validation);

    public static readonly Error DiscountCannotBeNegative =
        new("Insurance.DiscountCannotBeNegative", "Discount cannot be negative", ErrorType.Validation);
}