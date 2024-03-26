using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Doctors.GetDoctors;

public sealed class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, Result<PaginationResult<DoctorViewModel>>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result<PaginationResult<DoctorViewModel>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
    {
        var paginationDoctors = await _doctorRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

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