using MyClinic.Common.Models.Pagination;
using MyClinic.Common.Persistence.Repositories;
using MyClinic.Procedures.Domain.Entities;

namespace MyClinic.Procedures.Domain.Repositories;

public interface IProcedureRepository
    : IReadableRepository<Procedure>,
      ICreatableRepository<Procedure>,
      IUpdatableRepository<Procedure>
{
    Task<PaginationResult<Procedure>> GetAllBySpecialityAsync(Guid specialityId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<bool> IsUniqueAsync(string name, Guid specialityId, CancellationToken cancellationToken = default);
}