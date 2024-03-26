using System.Linq.Expressions;
using MyClinic.Common.Persistence.Repositories;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Domain.Repositories;

public interface IPatientRepository
    : IReadableRepository<Patient>,
      ICreatableRepository<Patient>,
      IUpdatableRepository<Patient>
{
    Task<bool> IsUniqueAsync(string cpf, string email, CancellationToken cancellationToken = default);
    Task<Patient?> GetByAsync(Expression<Func<Patient, bool>> predicate, CancellationToken cancellationToken = default);
}