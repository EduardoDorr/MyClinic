using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Application.Doctors.CreateDoctor;

public sealed class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ISpecialityRepository _specialityRepository;

    public CreateDoctorCommandHandler(
        IUnitOfWork unitOfWork,
        IDoctorRepository doctorRepository,
        ISpecialityRepository specialityRepository)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
    }

    public async Task<Result<Guid>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _doctorRepository.IsUniqueAsync(request.Cpf, request.Email, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(DoctorErrors.IsNotUnique);

        var specialitiesResult = await GetSpecialitiesAsync(request.SpecialitiesId, cancellationToken);

        if (!specialitiesResult.Success)
            return Result.Fail<Guid>(specialitiesResult.Errors);

        var doctorSchedules = GetDoctorSchedules(request.Schedules);

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
            .WithSchedules(doctorSchedules)
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

    private async Task<Result<List<Speciality>>> GetSpecialitiesAsync(List<Guid>? specialitiesId, CancellationToken cancellationToken)
    {
        if (specialitiesId is null || specialitiesId.Count == 0)
            return Result.Ok(new List<Speciality>());

        var tasks = specialitiesId.Select(id => _specialityRepository.GetByIdAsync(id, cancellationToken));
        var specialities = await Task.WhenAll(tasks);

        if (specialities.Any(s => s is null))
        {
            var specialitiesNotFound = specialitiesId.Except(specialities.Where(s => s is not null).Select(s => s.Id));
            var specialitiesErrors = specialitiesNotFound.Select(id => new Error("SpecialityNotFound", $"Not found id {id}", ErrorType.NotFound));

            return Result.Fail<List<Speciality>>(specialitiesErrors);
        }

        return Result.Ok(specialities.ToList());
    }

    private static List<DoctorSchedule> GetDoctorSchedules(List<ScheduleInputModel>? scheduleModels) =>
        scheduleModels?.Select(s => new DoctorSchedule(s.StartDate, s.EndDate))
                       .ToList() ?? new();
}