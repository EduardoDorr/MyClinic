using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Interfaces;

namespace MyClinic.Doctors.Application.Doctors.UpdateDoctor;

public sealed class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
    }

    public async Task<Result> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doctor is null)
            return Result.Fail(DoctorErrors.NotFound);

        var doctorResult = Doctor
            .CreateBuilder()
            .WithName(request.FirstName, request.LastName)
            .WithBirthDate(request.BirthDate)
            .WithDocument(doctor.Cpf.Number)
            .WithContactInfo(request.Email, request.Telephone)
            .WithAddress(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode)
            .WithMedicalInfo(request.BloodData.BloodType, request.BloodData.RhFactor, request.Gender)
            //.WithHeightAndWeight(request.Height, request.Weight)
            //.WithInsuranceId(request.InsuranceId)
            .Build();

        if (!doctorResult.Success)
            return Result.Fail(doctorResult.Errors);

        var doctorUpdated = doctorResult.Value;

        doctor.Update(doctorUpdated);

        _doctorRepository.Update(doctor);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(DoctorErrors.CannotBeUpdated);

        return Result.Ok();
    }
}