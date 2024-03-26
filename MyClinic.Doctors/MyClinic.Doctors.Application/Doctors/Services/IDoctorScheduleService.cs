using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Application.Doctors.Services;

public interface IDoctorScheduleService
{
    Task<Result<List<Schedule>>> GetSchedulesAsync(List<Guid>? schedulesId, CancellationToken cancellationToken);
}