using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyClinic.Common.Persistence.UnitOfWork;

public abstract class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork, IDisposable where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    protected IDbContextTransaction? _transaction;

    public BaseUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _dbContext.Dispose();
    }
}