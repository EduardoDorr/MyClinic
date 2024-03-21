using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Application.Patients.CreatePatient;

public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }

    public async Task<Result<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _patientRepository.IsUniqueAsync(request.Cpf, request.Email, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(PatientErrors.IsNotUnique);

        var patientResult = Patient
            .CreateBuilder()
            .WithName(request.FirstName, request.LastName)
            .WithBirthDate(request.BirthDate)
            .WithDocument(request.Cpf)
            .WithContactInfo(request.Email, request.Telephone)
            .WithAddress(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode)
            .WithMedicalInfo(request.BloodData.BloodType, request.BloodData.RhFactor, request.Gender)
            .WithHeightAndWeight(request.Height, request.Weight)
            .WithInsuranceId(request.InsuranceId)
            .Build();

        if (!patientResult.Success)
            return Result.Fail<Guid>(patientResult.Errors);

        var patient = patientResult.Value;

        _patientRepository.Create(patient);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(PatientErrors.CannotBeCreated);

        return Result.Ok(patient.Id);
    }
}