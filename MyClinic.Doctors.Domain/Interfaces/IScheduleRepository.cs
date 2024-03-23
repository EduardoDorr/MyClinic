using MyClinic.Common.Persistences.Repositories;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Domain.Interfaces;

public interface IScheduleRepository
    : IReadableRepository<Schedule>,
      ICreatableRepository<Schedule>
{
    Task<bool> IsUniqueAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}