namespace MyClinic.Common.Persistences.UnitOfWork;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}