using MyClinic.Common.Results.Errors;

namespace MyClinic.Appointments.Domain.Entities;

public record AppointmentErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error CannotBeCreated =
        new("Appointment.CannotBeCreated", "Something went wrong and the Appointment cannot be created", ErrorType.Failure);

    public static readonly Error CannotBeUpdated =
        new("Appointment.CannotBeUpdated", "Something went wrong and the Appointment cannot be updated", ErrorType.Failure);

    public static readonly Error CannotBeDeleted =
        new("Appointment.CannotBeDeleted", "Something went wrong and the Appointment cannot be deleted", ErrorType.Failure);

    public static readonly Error NotFound =
        new("Appointment.NotFound", "Could not find an active Appointment", ErrorType.NotFound);

    public static readonly Error IsNotUnique =
        new("Appointment.IsNotUnique", "A doctor's or patient's appointment conflicts with a existing one", ErrorType.Conflict);

    public static readonly Error AlreadyCanceled =
        new("Appointment.AlreadyCanceled", "Appointment is already canceled", ErrorType.Conflict);

    public static readonly Error CannotBeCanceled =
        new("Appointment.CannotBeCanceled", "Appointment only can be canceled while in scheduled, pending or confirmed status", ErrorType.Validation);

    public static readonly Error CannotBeRescheduled =
        new("Appointment.CannotBeRescheduled", "Appointment only can be recheduled while in scheduled, pending or confirmed status", ErrorType.Validation);

    public static readonly Error EndDateIsInvalid =
        new("Appointment.EndDateIsInvalid", "Appointment's End Date is earlier than the Start Date", ErrorType.Validation);

    public static readonly Error StatusDoesNotMatchMethod =
        new("Appointment.StatusDoesNotMatchMethod", "Appointment Status does not match the method requeriments", ErrorType.Validation);
}