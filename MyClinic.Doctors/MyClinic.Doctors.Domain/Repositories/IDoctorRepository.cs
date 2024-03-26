using System.Linq.Expressions;
using MyClinic.Common.Models.Pagination;
using MyClinic.Common.Persistence.Repositories;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Domain.Repositories;

public interface IDoctorRepository
    : IReadableRepository<Doctor>,
      ICreatableRepository<Doctor>,
      IUpdatableRepository<Doctor>
{
    Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByAsync(Expression<Func<Doctor, bool>> predicate, CancellationToken cancellationToken = default);
    Task<PaginationResult<Doctor>> GetAllBySpecialityAsync(Guid specialityId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}