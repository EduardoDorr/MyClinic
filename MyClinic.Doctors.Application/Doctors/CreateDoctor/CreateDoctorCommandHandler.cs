using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Services;

namespace MyClinic.Doctors.Application.Doctors.CreateDoctor;

public sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDoctorSpecialityService _doctorSpecialityService;

    public CreateDoctorCommandHandler(
        IUnitOfWork unitOfWork,
        IDoctorRepository doctorRepository,
        IDoctorSpecialityService doctorSpecialityService)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
        _doctorSpecialityService = doctorSpecialityService;
    }

    public async Task<Result<Guid>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _doctorRepository.IsUniqueAsync(request.Cpf, request.Email, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(DoctorErrors.IsNotUnique);

        var specialitiesResult = await _doctorSpecialityService.GetSpecialitiesAsync(request.SpecialitiesId, cancellationToken);

        if (!specialitiesResult.Success)
            return Result.Fail<Guid>(specialitiesResult.Errors);

        var doctorResult = Doctor
            .CreateBuilder()
            .WithName(request.FirstName, request.LastName)
            .WithBirthDate(request.BirthDate)
            .WithDocument(request.Cpf)
            .WithContactInfo(request.Email, request.Telephone)
            .WithAddress(request.Address.Street, request.Address.City, request.Address.State, request.Address.Country, request.Address.ZipCode)
            .WithMedicalInfo(request.BloodData.BloodType, request.BloodData.RhFactor, request.Gender)
            .WithLicenseNumber(request.LicenseNumber)
            .WithSpecialities(specialitiesResult.Value)
            .WithSchedules(request.Schedules)
            .Build();

        if (!doctorResult.Success)
            return Result.Fail<Guid>(doctorResult.Errors);

        var doctor = doctorResult.Value;

        _doctorRepository.Create(doctor);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(DoctorErrors.CannotBeCreated);

        return Result.Ok(doctor.Id);
    }
}