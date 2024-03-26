using System.Linq.Expressions;

using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Domain.Repositories;

namespace MyClinic.Patients.Application.Patients.GetPatientBy;

public sealed class GetPatientByQueryHandler : IRequestHandler<GetPatientByQuery, Result<PatientDetailsViewModel?>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientDetailsViewModel?>> Handle(GetPatientByQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByAsync(GetExpression(request), cancellationToken);

        if (patient is null)
            return Result.Fail<PatientDetailsViewModel?>(PatientErrors.NotFound);

        var patientViewModel = patient?.ToDetailsViewModel();

        return Result.Ok(patientViewModel);
    }

    private static Expression<Func<Patient, bool>> GetExpression(GetPatientByQuery request)
    {
        return p => p.Cpf.Number == request.Cpf ||
                    p.Email.Address == request.Email;
    }
}