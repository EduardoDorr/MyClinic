using MyClinic.Common.Results.Errors;

namespace MyClinic.Doctors.Domain.Entities.Specialities;

public sealed record SpecialityErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Speciality.CannotBeCreated", "Something went wrong and the Speciality cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Speciality.CannotBeUpdated", "Something went wrong and the Speciality cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Speciality.CannotBeDeleted", "Something went wrong and the Speciality cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Speciality.NotFound", "Not found", ErrorType.NotFound);

    public static readonly Error NameIsRequired =
        new("Speciality.NameIsRequired", "Name is required", ErrorType.Validation);

    public static readonly Error IsNotUnique =
        new("Speciality.IsNotUnique", "The Speciality's Name is already taken", ErrorType.Conflict);
}