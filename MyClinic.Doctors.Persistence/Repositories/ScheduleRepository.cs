using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Schedules;
using MyClinic.Doctors.Persistence.Contexts;

namespace MyClinic.Doctors.Persistence.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly MyClinicDoctorDbContext _dbContext;

    public ScheduleRepository(MyClinicDoctorDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PaginationResult<Schedule>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var specialities = _dbContext.Schedules.AsQueryable();

        return await specialities.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Schedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Schedules.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var hasSchedule =
            await _dbContext.Schedules
            .AnyAsync(s => (startDate <= s.StartDate && endDate > s.StartDate) ||
                           (startDate < s.EndDate && endDate >= s.EndDate) ||
                           (startDate >= s.StartDate && endDate <= s.EndDate) ||
                           (startDate < s.StartDate && endDate > s.EndDate),
                      cancellationToken);

        return !hasSchedule;
    }

    public void Create(Schedule schedule)
    {
        _dbContext.Schedules.Add(schedule);
    }
}