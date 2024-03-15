using MyClinic.Common.Persistences.Repositories;
using MyClinic.Patients.Domain.Entities.Insurances;

namespace MyClinic.Patients.Domain.Interfaces;

public interface IInsuranceRepository
    : IReadableRepository<Insurance>,
      ICreatableRepository<Insurance>,
      IUpdatableRepository<Insurance>
{ }