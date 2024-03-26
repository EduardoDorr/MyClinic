﻿using Microsoft.EntityFrameworkCore.Storage;

using MyClinic.Procedures.Domain.UnitOfWork;
using MyClinic.Procedures.Persistence.Contexts;

namespace MyClinic.Procedures.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    protected readonly MyClinicProcedureDbContext _dbContext;
    protected IDbContextTransaction? _transaction;

    public UnitOfWork(MyClinicProcedureDbContext dbContext)
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