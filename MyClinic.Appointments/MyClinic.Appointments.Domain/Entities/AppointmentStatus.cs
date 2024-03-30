namespace MyClinic.Appointments.Domain.Entities;

public enum AppointmentStatus
{
    Pending = 1,
    Scheduled = 2,
    CanceledByPatient = 4,
    CanceledByDoctor = 5,
    NotAttended = 6,
    Rescheduled = 7,
    InProgress = 8,
    Completed = 9
}