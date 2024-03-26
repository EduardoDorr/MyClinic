using MyClinic.Common.Persistence.Repositories;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Domain.Repositories;

public interface ISpecialityRepository
    : IReadableRepository<Speciality>,
      ICreatableRepository<Speciality>,
      IUpdatableRepository<Speciality>
{
    Task<bool> IsUniqueAsync(string name, CancellationToken cancellationToken = default);
}