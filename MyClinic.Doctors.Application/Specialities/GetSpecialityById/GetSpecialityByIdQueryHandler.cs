using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Application.Specialities.Models;

namespace MyClinic.Doctors.Application.Specialities.GetSpecialityById;

public sealed class GetSpecialityByIdQueryHandler : IRequestHandler<GetSpecialityByIdQuery, Result<SpecialityDetailsViewModel?>>
{
    private readonly ISpecialityRepository _specialityRepository;

    public GetSpecialityByIdQueryHandler(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public async Task<Result<SpecialityDetailsViewModel?>> Handle(GetSpecialityByIdQuery request, CancellationToken cancellationToken)
    {
        var speciality = await _specialityRepository.GetByIdAsync(request.Id, cancellationToken);

        if (speciality is null)
            return Result.Fail<SpecialityDetailsViewModel?>(SpecialityErrors.NotFound);

        var specialityDetailsViewModel = speciality?.ToDetailsViewModel();

        return Result.Ok(specialityDetailsViewModel);
    }
}