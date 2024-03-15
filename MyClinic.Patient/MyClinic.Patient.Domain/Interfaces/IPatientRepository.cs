using MyClinic.Common.Persistences.Repositories;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Domain.Interfaces;

public interface IPatientRepository
    : IReadableRepository<Patient>,
      ICreatableRepository<Patient>,
      IUpdatableRepository<Patient>
{ }