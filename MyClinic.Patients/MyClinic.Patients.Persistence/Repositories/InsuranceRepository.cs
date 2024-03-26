using Microsoft.EntityFrameworkCore;
using MyClinic.Patients.Domain.Entities.Insurances;
using MyClinic.Patients.Persistence.Contexts;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Domain.Repositories;

namespace MyClinic.Patients.Persistence.Repositories;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly MyClinicPatientDbContext _dbContext;

    public InsuranceRepository(MyClinicPatientDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PaginationResult<Insurance>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var insurances = _dbContext.Insurances.AsQueryable();

        return await insurances.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Insurance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Insurances.SingleOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public void Create(Insurance insurance)
    {
        _dbContext.Insurances.Add(insurance);
    }

    public void Update(Insurance insurance)
    {
        _dbContext.Insurances.Update(insurance);
    }
}