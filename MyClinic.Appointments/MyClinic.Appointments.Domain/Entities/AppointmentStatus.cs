namespace MyClinic.Appointments.Domain.Entities;

public enum AppointmentStatus
{
    Scheduled = 1,
    Pending = 2,
    Confirmed = 3,
    CanceledByPatient = 4,
    CanceledByDoctor = 5,
    NotAttended = 6,
    Rescheduled = 7,
    InProgress = 8,
    Completed = 9
}