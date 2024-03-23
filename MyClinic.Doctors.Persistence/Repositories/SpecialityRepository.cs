using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Persistence.Contexts;

namespace MyClinic.Doctors.Persistence.Repositories;

public class SpecialityRepository : ISpecialityRepository
{
    private readonly MyClinicDoctorDbContext _dbContext;

    public SpecialityRepository(MyClinicDoctorDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PaginationResult<Speciality>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var specialities = _dbContext.Specialities.AsQueryable();

        return await specialities.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Speciality?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Specialities.Include(s => s.Doctors).SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        var hasSpeciality = await _dbContext.Specialities.AnyAsync(s => s.Name == name, cancellationToken);

        return !hasSpeciality;
    }

    public void Create(Speciality speciality)
    {
        _dbContext.Specialities.Add(speciality);
    }

    public void Update(Speciality speciality)
    {
        _dbContext.Specialities.Update(speciality);
    }    
}