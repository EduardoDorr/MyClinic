using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatient;

public sealed class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, Result<PatientViewModel?>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientViewModel?>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null)
            return Result.Fail<PatientViewModel?>(PatientErrors.NotFound);

        var patientViewModel = patient?.ToViewModel();

        return Result.Ok(patientViewModel);
    }
}