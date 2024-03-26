using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Persistence.Contexts;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly MyClinicDoctorDbContext _dbContext;

    public DoctorRepository(MyClinicDoctorDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PaginationResult<Doctor>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var doctors = _dbContext.Doctors.AsQueryable();

        return await doctors.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<PaginationResult<Doctor>> GetAllBySpecialityAsync(Guid specialityId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var doctors = _dbContext.Doctors
            .Include(d => d.Specialities)
            .Where(d => d.Specialities.Any(s => s.Id == specialityId))
            .AsQueryable();

        return await doctors.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors
            .Include(d => d.Specialities)
            .Include(d => d.Schedules)
            .SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Doctor?> GetByAsync(Expression<Func<Doctor, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Doctors
            .Include(d => d.Specialities)
            .Include(d => d.Schedules)
            .SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default)
    {
        var hasDoctor = await _dbContext.Doctors.AnyAsync(p => p.Cpf.Number == cpf || p.Email.Address == email, cancellationToken);

        return !hasDoctor;
    }

    public void Create(Doctor doctor)
    {
        _dbContext.Doctors.Add(doctor);
    }

    public void Update(Doctor doctor)
    {
        _dbContext.Doctors.Update(doctor);
    }
}