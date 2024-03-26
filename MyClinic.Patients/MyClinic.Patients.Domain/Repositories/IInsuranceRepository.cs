using MyClinic.Common.Persistence.Repositories;
using MyClinic.Patients.Domain.Entities.Insurances;

namespace MyClinic.Patients.Domain.Repositories;

public interface IInsuranceRepository
    : IReadableRepository<Insurance>,
      ICreatableRepository<Insurance>,
      IUpdatableRepository<Insurance>
{ }