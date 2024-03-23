using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Application.Doctors.AddSchedules;

public sealed class AddSchedulesCommandHandler : IRequestHandler<AddSchedulesCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public AddSchedulesCommandHandler(
        IUnitOfWork unitOfWork,
        IDoctorRepository doctorRepository,
        IScheduleRepository scheduleRepository)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<Result> Handle(AddSchedulesCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken);

        if (doctor is null)
            return Result.Fail(DoctorErrors.NotFound);

        var uniquenessResult = await CheckSchedulesForUniquenessAsync(request.Schedules, cancellationToken);

        if (!uniquenessResult.Success)
            return Result.Fail(uniquenessResult.Errors);

        var schedulesResult = CreateSchedules(request.Schedules, request.DoctorId);

        if (!schedulesResult.Success)
            return Result.Fail(schedulesResult.Errors);

        doctor.AddSchedules(schedulesResult.Value);

        foreach (var schedule in schedulesResult.Value)
            _scheduleRepository.Create(schedule);

        _doctorRepository.Update(doctor);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(DoctorErrors.CannotBeUpdated);

        return Result.Ok();
    }

    private async Task<Result> CheckSchedulesForUniquenessAsync(IList<DoctorSchedule> doctorSchedules, CancellationToken cancellationToken)
    {
        if (doctorSchedules is null || doctorSchedules.Count == 0)
            return Result.Ok();

        var notUniqueDoctorSchedules = new List<DoctorSchedule>();

        foreach (var doctorSchedule in doctorSchedules)
        {
            var isUnique = await _scheduleRepository.IsUniqueAsync(doctorSchedule.StartDate, doctorSchedule.EndDate, cancellationToken);

            if (!isUnique)
                notUniqueDoctorSchedules.Add(doctorSchedule);
        }

        if (notUniqueDoctorSchedules.Count > 0)
        {
            var doctorSchedulesErrors =
                notUniqueDoctorSchedules
                .Select(ds =>
                    new Error(
                        "ScheduleIsNotUnique",
                        $"Schedule that starts at {ds.StartDate} and ends at {ds.EndDate} conflicts with an existing one",
                        ErrorType.Conflict));

            return Result.Fail(doctorSchedulesErrors);
        }

        return Result.Ok();
    }

    private static Result<List<Schedule>> CreateSchedules(List<DoctorSchedule> doctorSchedules, Guid doctorId)
    {
        var schedulesResult = doctorSchedules.Select(s => Schedule.Create(s.StartDate, s.EndDate, doctorId));

        if (schedulesResult.Any(s => !s.Success))
            return Result.Fail<List<Schedule>>(schedulesResult.SelectMany(s => s.Errors));

        var schedules = schedulesResult.Select(s => s.Value).ToList();

        return Result.Ok(schedules);
    }
}