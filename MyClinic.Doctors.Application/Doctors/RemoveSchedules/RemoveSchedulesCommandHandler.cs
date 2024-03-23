using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Services;

namespace MyClinic.Doctors.Application.Doctors.RemoveSchedules;

public sealed class RemoveSchedulesCommandHandler : IRequestHandler<RemoveSchedulesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDoctorScheduleService _doctorScheduleService;

    public RemoveSchedulesCommandHandler(
        IUnitOfWork unitOfWork,
        IDoctorRepository doctorRepository,
        IDoctorScheduleService doctorScheduleService)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
        _doctorScheduleService = doctorScheduleService;
    }

    public async Task<Result> Handle(RemoveSchedulesCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken);

        if (doctor is null)
            return Result.Fail(DoctorErrors.NotFound);

        var schedulesResult = await _doctorScheduleService.GetSchedulesAsync(request.SchedulesId, cancellationToken);

        if (!schedulesResult.Success)
            return Result.Fail(schedulesResult.Errors);

        doctor.RemoveSchedules(schedulesResult.Value);

        _doctorRepository.Update(doctor);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(DoctorErrors.CannotBeUpdated);

        return Result.Ok();
    }
}