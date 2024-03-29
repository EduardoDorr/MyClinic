namespace MyClinic.Appointments.Application.Appointments.UpdateAppointment;

public sealed record UpdateAppointmentInputModel(
    DateTime StartDate);

public static class UpdateDoctorInputModelExtension
{
    public static UpdateAppointmentCommand ToCommand(this UpdateAppointmentInputModel model, Guid id)
    {
        return new UpdateAppointmentCommand(
            id,
            model.StartDate);
    }
}