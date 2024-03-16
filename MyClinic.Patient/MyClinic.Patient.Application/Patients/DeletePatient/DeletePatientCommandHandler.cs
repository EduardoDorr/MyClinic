using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Application.Patients.DeletePatient;

public sealed class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;

    public DeletePatientCommandHandler(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }

    public async Task<Result> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null)
            return Result.Fail(PatientErrors.NotFound);

        patient.Deactivate();

        _patientRepository.Update(patient);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(PatientErrors.CannotBeDeleted);

        return Result.Ok();
    }
}