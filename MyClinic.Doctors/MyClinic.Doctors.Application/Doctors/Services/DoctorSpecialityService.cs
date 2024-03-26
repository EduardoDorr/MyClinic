using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Doctors.Services;

public class DoctorSpecialityService : IDoctorSpecialityService
{
    private readonly ISpecialityRepository _specialityRepository;

    public DoctorSpecialityService(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public async Task<Result<List<Speciality>>> GetSpecialitiesAsync(List<Guid>? specialitiesId, CancellationToken cancellationToken)
    {
        if (specialitiesId is null || specialitiesId.Count == 0)
            return Result.Ok(new List<Speciality>());

        var specialities = new List<Speciality>();

        foreach (var specialityId in specialitiesId)
        {
            var speciality = await _specialityRepository.GetByIdAsync(specialityId, cancellationToken);
            specialities.Add(speciality);
        }

        if (specialities.Any(s => s is null))
        {
            var specialitiesNotFound = specialitiesId.Except(specialities.Where(s => s is not null).Select(s => s.Id));
            var specialitiesErrors = specialitiesNotFound.Select(id => new Error("SpecialityNotFound", $"Not found id {id}", ErrorType.NotFound));

            return Result.Fail<List<Speciality>>(specialitiesErrors);
        }

        return Result.Ok(specialities.ToList());
    }
}