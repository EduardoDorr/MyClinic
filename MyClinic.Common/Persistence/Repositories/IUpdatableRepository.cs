using MyClinic.Common.Entities;

namespace MyClinic.Common.Persistence.Repositories;

public interface IUpdatableRepository<TEntity> where TEntity : BaseEntity
{
    void Update(TEntity entity);
}