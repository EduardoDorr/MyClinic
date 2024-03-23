using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Application.Doctors.Services;

public class DoctorScheduleService : IDoctorScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;

    public DoctorScheduleService(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Result<List<Schedule>>> GetSchedulesAsync(List<Guid>? schedulesId, CancellationToken cancellationToken)
    {
        if (schedulesId is null || schedulesId.Count == 0)
            return Result.Ok(new List<Schedule>());

        var schedules = new List<Schedule>();

        foreach (var scheduleId in schedulesId)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId, cancellationToken);
            schedules.Add(schedule);
        }

        if (schedules.Any(s => s is null))
        {
            var schedulesNotFound = schedulesId.Except(schedules.Where(s => s is not null).Select(s => s.Id));
            var schedulesErrors = schedulesNotFound.Select(id => new Error("ScheduleNotFound", $"Not found id {id}", ErrorType.NotFound));

            return Result.Fail<List<Schedule>>(schedulesErrors);
        }

        return Result.Ok(schedules.ToList());
    }
}