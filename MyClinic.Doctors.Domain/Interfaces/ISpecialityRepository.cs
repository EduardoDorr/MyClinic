using MyClinic.Common.Persistences.Repositories;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Domain.Interfaces;

public interface ISpecialityRepository
    : IReadableRepository<Speciality>,
      ICreatableRepository<Speciality>,
      IUpdatableRepository<Speciality>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}