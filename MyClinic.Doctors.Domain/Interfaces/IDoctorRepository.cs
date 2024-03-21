using MyClinic.Common.Persistences.Repositories;
using MyClinic.Doctors.Domain.Entities.Doctors;
using System.Linq.Expressions;

namespace MyClinic.Doctors.Domain.Interfaces;

public interface IDoctorRepository
    : IReadableRepository<Doctor>,
      ICreatableRepository<Doctor>,
      IUpdatableRepository<Doctor>
{
    Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByAsync(Expression<Func<Doctor, bool>> predicate, CancellationToken cancellationToken = default);
}