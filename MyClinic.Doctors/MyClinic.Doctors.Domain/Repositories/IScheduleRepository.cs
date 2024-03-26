using MyClinic.Common.Persistence.Repositories;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Domain.Repositories;

public interface IScheduleRepository
    : IReadableRepository<Schedule>,
      ICreatableRepository<Schedule>
{
    Task<bool> IsUniqueAsync(DateTime startDate, DateTime endDate, Guid doctorId, CancellationToken cancellationToken = default);
}