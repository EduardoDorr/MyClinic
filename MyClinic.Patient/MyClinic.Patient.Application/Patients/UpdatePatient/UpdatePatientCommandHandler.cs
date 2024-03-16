using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Application.Patients.UpdatePatient;

public sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }

    public async Task<Result> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null)
            return Result.Fail(PatientErrors.NotFound);

        var patientResult = Patient
            .CreateBuilder()
            .WithName(request.FirstName, request.LastName)
            .WithBirthDate(request.BirthDate)
            .WithDocument(patient.Cpf.Number)
            .WithContactInfo(request.Email, request.Telephone)
            .WithAddress(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode)
            .WithMedicalInfo(request.BloodData.BloodType, request.BloodData.RhFactor, request.Gender, request.Height, request.Weight)
            .WithInsuranceId(request.InsuranceId)
            .Build();

        if (!patientResult.Success)
            return Result.Fail(patientResult.Errors);

        var patientUpdated = patientResult.Value;

        patient.Update(patientUpdated);

        _patientRepository.Update(patient);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(PatientErrors.CannotBeUpdated);

        return Result.Ok();
    }
}