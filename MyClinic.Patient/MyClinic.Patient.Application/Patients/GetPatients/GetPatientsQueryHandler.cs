using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatients;

public sealed class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, Result<PaginationResult<PatientViewModel>>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientsQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PaginationResult<PatientViewModel>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var paginationPatients = await _patientRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var patientsViewModel = paginationPatients.Data.ToViewModel();

        var paginationPatientsViewModel =
            new PaginationResult<PatientViewModel>
            (
                paginationPatients.Page,
                paginationPatients.PageSize,
                paginationPatients.TotalCount,
                paginationPatients.TotalPages,
                patientsViewModel.ToList()
            );

        return Result.Ok(paginationPatientsViewModel);
    }
}