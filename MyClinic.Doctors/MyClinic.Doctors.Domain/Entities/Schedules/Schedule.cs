using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Domain.Entities.Schedules;

public class Schedule : BaseEntity
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Guid DoctorId { get; private set; }

    public virtual Doctor Doctor { get; set; }

    protected Schedule() { }

    private Schedule(DateTime startDate, DateTime endDate, Guid doctorId)
    {
        StartDate = startDate;
        EndDate = endDate;
        DoctorId = doctorId;
    }

    public static Result<Schedule> Create(DateTime startDate, DateTime endDate, Guid doctorId)
    {
        if (endDate.CompareTo(startDate) <= 0)
            return Result.Fail<Schedule>(ScheduleErrors.EndDateMustBeGreaterThanStartDate);

        var schedule = new Schedule(startDate, endDate, doctorId);

        return Result.Ok(schedule);
    }
}