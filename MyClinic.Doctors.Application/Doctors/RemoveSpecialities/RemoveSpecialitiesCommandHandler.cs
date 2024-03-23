using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Services;

namespace MyClinic.Doctors.Application.Doctors.RemoveSpecialities;

public sealed class RemoveSchedulesCommandHandler : IRequestHandler<RemoveSpecialitiesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDoctorSpecialityService _doctorSpecialityService;

    public RemoveSchedulesCommandHandler(
        IUnitOfWork unitOfWork,
        IDoctorRepository doctorRepository,
        IDoctorSpecialityService doctorSpecialityService)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
        _doctorSpecialityService = doctorSpecialityService;
    }

    public async Task<Result> Handle(RemoveSpecialitiesCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken);

        if (doctor is null)
            return Result.Fail(DoctorErrors.NotFound);

        var specialitiesResult = await _doctorSpecialityService.GetSpecialitiesAsync(request.SpecialitiesId, cancellationToken);

        if (!specialitiesResult.Success)
            return Result.Fail(specialitiesResult.Errors);

        doctor.RemoveSpecialities(specialitiesResult.Value);

        _doctorRepository.Update(doctor);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(DoctorErrors.CannotBeUpdated);

        return Result.Ok();
    }
}