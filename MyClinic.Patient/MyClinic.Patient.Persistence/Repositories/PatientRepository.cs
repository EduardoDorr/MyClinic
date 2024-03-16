using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Persistence.Contexts;

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
        return await _dbContext.Patients.Include(p => p.Insurance).SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default)
    {
        var hasPatient = await _dbContext.Patients.AnyAsync(p => p.Cpf.Number == cpf || p.Email.Address == email, cancellationToken);

        return !hasPatient;
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