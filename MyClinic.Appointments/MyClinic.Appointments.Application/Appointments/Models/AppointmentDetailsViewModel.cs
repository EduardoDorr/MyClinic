using MyClinic.Appointments.Domain.Entities;

namespace MyClinic.Appointments.Application.Appointments.Models;

public record AppointmentDetailsViewModel(
    Guid Id,
    Guid PatientId,
    Guid DoctorId,
    Guid ProcedureId,
    DateTime ScheduledStartDate,
    DateTime ScheduledEndDate,
    DateTime? RealStartDate,
    DateTime? RealEndDate,
    DateTime? CancellationDate,
    AppointmentType Type,
    AppointmentStatus Status);

public static class AppointmentDetailsViewModelExtension
{
    public static AppointmentDetailsViewModel ToDetailsViewModel(this Appointment appointment)
    {
        return new AppointmentDetailsViewModel(
            appointment.Id,
            appointment.PatientId,
            appointment.DoctorId,
            appointment.ProcedureId,
            appointment.ScheduledStartDate,
            appointment.ScheduledEndDate,
            appointment.RealStartDate,
            appointment.RealEndDate,
            appointment.CancellationDate,
            appointment.Type,
            appointment.Status);
    }

    public static ICollection<AppointmentDetailsViewModel> ToDetailsViewModel(this IEnumerable<Appointment> appointments)
    {
        return appointments is not null
             ? appointments.Select(fg => fg.ToDetailsViewModel()).ToList()
             : [];
    }
}