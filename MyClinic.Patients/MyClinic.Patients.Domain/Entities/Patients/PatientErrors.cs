﻿using MyClinic.Common.Results.Errors;

namespace MyClinic.Patients.Domain.Entities.Patients;

public record PatientErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Patient.CannotBeCreated", "Something went wrong and the Patient cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Patient.CannotBeUpdated", "Something went wrong and the Patient cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Patient.CannotBeDeleted", "Something went wrong and the Patient cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Patient.NotFound", "Could not find an active Patient", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Patient.IsNotUnique", "The patient's CPF or Email is already taken", ErrorType.Conflict);
}