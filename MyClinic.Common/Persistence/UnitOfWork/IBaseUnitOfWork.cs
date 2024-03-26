namespace MyClinic.Common.Persistence.UnitOfWork;

public interface IBaseUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}