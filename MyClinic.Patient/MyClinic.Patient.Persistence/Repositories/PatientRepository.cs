using Microsoft.EntityFrameworkCore;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Persistence.Contexts;
using MyClinic.Common.Models.Pagination;

namespace MyClinic.Patients.Persistence.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly MyClinicPatientDbContext _dbContext;

    public PatientRepository(MyClinicPatientDbContext context)
    {
        _dbContext = context;
    }    

    public async Task<PaginationResult<Patient>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var patients = _dbContext.Patients.AsQueryable();

        return await patients.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Patients.Include(f => f.Insurance).SingleOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public void Create(Patient patient)
    {
        _dbContext.Patients.Add(patient);
    }

    public void Update(Patient patient)
    {
        _dbContext.Patients.Update(patient);
    }
}