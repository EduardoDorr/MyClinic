using MyClinic.Common.Results.Errors;

namespace MyClinic.Procedures.Domain.Entities;

public record ProcedureErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Procedure.CannotBeCreated", "Something went wrong and the Procedure cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Procedure.CannotBeUpdated", "Something went wrong and the Procedure cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Procedure.CannotBeDeleted", "Something went wrong and the Procedure cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Procedure.NotFound", "Could not find an active Procedure", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Procedure.IsNotUnique", "The Procedure's name is already taken", ErrorType.Conflict);
}