namespace MyClinic.Appointments.Domain.Entities;

public enum AppointmentType
{
    None = 0,
    InitialAppointment = 1,
    FollowUpAppointment = 2,
    RoutineAppointment = 3,
    LongTermTreatment = 4,
    UrgentCare = 5
}