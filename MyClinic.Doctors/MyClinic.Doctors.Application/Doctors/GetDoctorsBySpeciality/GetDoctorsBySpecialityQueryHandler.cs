using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorsBySpeciality;

public sealed class GetDoctorsBySpecialityQueryHandler : IRequestHandler<GetDoctorsBySpecialityQuery, Result<PaginationResult<DoctorViewModel>>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsBySpecialityQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result<PaginationResult<DoctorViewModel>>> Handle(GetDoctorsBySpecialityQuery request, CancellationToken cancellationToken)
    {
        var paginationDoctors = await _doctorRepository.GetAllBySpecialityAsync(request.SpecialityId, request.Page, request.PageSize, cancellationToken);

        var doctorsViewModel = paginationDoctors.Data.ToViewModel();

        var paginationDoctorsViewModel =
            new PaginationResult<DoctorViewModel>
            (
                paginationDoctors.Page,
                paginationDoctors.PageSize,
                paginationDoctors.TotalCount,
                paginationDoctors.TotalPages,
                doctorsViewModel.ToList()
            );

        return Result.Ok(paginationDoctorsViewModel);
    }
}