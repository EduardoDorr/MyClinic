using MyClinic.Common.Entities;

namespace MyClinic.Common.Persistence.Repositories;

public interface IDeletableRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(TEntity entity);
}