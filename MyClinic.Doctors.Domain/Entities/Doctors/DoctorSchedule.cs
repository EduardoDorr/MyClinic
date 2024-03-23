using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Domain.Entities.Doctors;

public sealed record DoctorSchedule(DateTime StartDate, DateTime EndDate);

public static class DoctorScheduleExtension
{
    public static DoctorSchedule ToDoctorSchedule(this Schedule schedule)
    {
        return new DoctorSchedule(
            schedule.StartDate,
            schedule.EndDate);
    }

    public static ICollection<DoctorSchedule> ToDoctorSchedule(this IEnumerable<Schedule> schedules)
    {
        return schedules is not null
             ? schedules.Select(fg => fg.ToDoctorSchedule()).ToList()
             : [];
    }
}