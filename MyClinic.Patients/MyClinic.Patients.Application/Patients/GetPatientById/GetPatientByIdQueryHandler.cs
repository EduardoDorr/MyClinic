using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Domain.Repositories;

namespace MyClinic.Patients.Application.Patients.GetPatientById;

public sealed class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result<PatientDetailsViewModel?>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientDetailsViewModel?>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null)
            return Result.Fail<PatientDetailsViewModel?>(PatientErrors.NotFound);

        var patientViewModel = patient?.ToDetailsViewModel();

        return Result.Ok(patientViewModel);
    }
}