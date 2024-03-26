using MyClinic.Common.Entities;

namespace MyClinic.Common.Persistence.Repositories;

public interface ICreatableRepository<TEntity> where TEntity : BaseEntity
{
    void Create(TEntity entity);
}