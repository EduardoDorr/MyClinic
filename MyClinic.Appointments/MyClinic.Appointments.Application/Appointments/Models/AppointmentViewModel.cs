using MyClinic.Appointments.Domain.Entities;

namespace MyClinic.Appointments.Application.Appointments.Models;

public record AppointmentViewModel(
    Guid Id,
    Guid PatientId,
    Guid DoctorId,
    Guid ProcedureId,
    DateTime StartDate,
    AppointmentType Type);

public static class AppointmentViewModelExtension
{
    public static AppointmentViewModel ToViewModel(this Appointment appointment)
    {
        return new AppointmentViewModel(
            appointment.Id,
            appointment.PatientId,
            appointment.DoctorId,
            appointment.ProcedureId,
            appointment.ScheduledStartDate,
            appointment.Type);
    }

    public static ICollection<AppointmentViewModel> ToViewModel(this IEnumerable<Appointment> appointments)
    {
        return appointments is not null
             ? appointments.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}