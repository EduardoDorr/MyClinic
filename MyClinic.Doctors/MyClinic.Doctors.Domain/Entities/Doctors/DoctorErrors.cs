using MyClinic.Common.Results.Errors;

namespace MyClinic.Doctors.Domain.Entities.Doctors;

public record DoctorErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Doctor.CannotBeCreated", "Something went wrong and the Doctor cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Doctor.CannotBeUpdated", "Something went wrong and the Doctor cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Doctor.CannotBeDeleted", "Something went wrong and the Doctor cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Doctor.NotFound", "Could not find an active Doctor", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Doctor.IsNotUnique", "The Doctor's CPF or Email is already taken", ErrorType.Conflict);
}