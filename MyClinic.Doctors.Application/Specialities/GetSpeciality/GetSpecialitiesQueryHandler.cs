using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Application.Specialities.Models;

namespace MyClinic.Doctors.Application.Specialities.GetSpeciality;

public sealed class GetSpecialitiesQueryHandler : IRequestHandler<GetSpecialitiesQuery, Result<PaginationResult<SpecialityViewModel>>>
{
    private readonly ISpecialityRepository _specialityRepository;

    public GetSpecialitiesQueryHandler(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public async Task<Result<PaginationResult<SpecialityViewModel>>> Handle(GetSpecialitiesQuery request, CancellationToken cancellationToken)
    {
        var paginationSpecialities = await _specialityRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var specialitiesViewModel = paginationSpecialities.Data.ToViewModel();

        var paginationSpecialitiesViewModel =
            new PaginationResult<SpecialityViewModel>
            (
                paginationSpecialities.Page,
                paginationSpecialities.PageSize,
                paginationSpecialities.TotalCount,
                paginationSpecialities.TotalPages,
                specialitiesViewModel.ToList()
            );

        return Result.Ok(paginationSpecialitiesViewModel);
    }
}