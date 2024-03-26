using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Domain.Repositories;
using MyClinic.Procedures.Persistence.Contexts;

namespace MyClinic.Procedures.Persistence.Repositories;

public class ProcedureRepository : IProcedureRepository
{
    private readonly MyClinicProcedureDbContext _dbContext;

    public ProcedureRepository(MyClinicProcedureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Procedure>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var procedures = _dbContext.Procedures.AsQueryable();

        return await procedures.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Procedure?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Procedures.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PaginationResult<Procedure>> GetAllBySpecialityAsync(Guid specialityId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var procedures = _dbContext.Procedures.Where(p => p.SpecialityId == specialityId).AsQueryable();

        return await procedures.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<bool> IsUniqueAsync(string name, Guid specialityId, CancellationToken cancellationToken = default)
    {
        var hasProcedure = await _dbContext.Procedures.AnyAsync(p => p.Name == name && p.SpecialityId == specialityId, cancellationToken);
        
        return !hasProcedure;
    }

    public void Create(Procedure procedures)
    {
        _dbContext.Procedures.Add(procedures);
    }    

    public void Update(Procedure procedures)
    {
        _dbContext.Procedures.Update(procedures);
    }
}