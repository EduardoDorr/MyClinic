using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Application.Doctors.Services;

public interface IDoctorSpecialityService
{
    Task<Result<List<Speciality>>> GetSpecialitiesAsync(List<Guid>? specialitiesId, CancellationToken cancellationToken);
}