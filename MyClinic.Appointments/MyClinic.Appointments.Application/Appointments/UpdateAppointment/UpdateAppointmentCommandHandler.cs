using MediatR;

using MyClinic.Common.Results;

using MyClinic.Doctors.Application.Doctors.Services;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Patients.Application.Patients.Services;
using MyClinic.Patients.Application.Patients.GetPatientById;
using MyClinic.Procedures.Application.Procedures.Services;
using MyClinic.Procedures.Application.Procedures.GetProcedureById;

using MyClinic.Appointments.Domain.Events;
using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Domain.UnitOfWork;
using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Domain.Events.Shared;

namespace MyClinic.Appointments.Application.Appointments.UpdateAppointment;

public sealed class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IProcedureService _procedureService;

    public UpdateAppointmentCommandHandler(
        IUnitOfWork unitOfWork,
        IAppointmentRepository appointmentRepository,
        IPatientService patientService,
        IDoctorService doctorService,
        IProcedureService procedureService)
    {
        _unitOfWork = unitOfWork;
        _appointmentRepository = appointmentRepository;
        _patientService = patientService;
        _doctorService = doctorService;
        _procedureService = procedureService;
    }

    public async Task<Result<Guid>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var currentAppointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (currentAppointment is null)
            return Result.Fail<Guid>(AppointmentErrors.NotFound);

        var patientResult = await _patientService.GetByIdAsync(new GetPatientByIdQuery(currentAppointment.PatientId));

        if (!patientResult.Success)
            return Result.Fail<Guid>(patientResult.Errors);

        var doctorResult = await _doctorService.GetByIdAsync(new GetDoctorByIdQuery(currentAppointment.DoctorId));

        if (!doctorResult.Success)
            return Result.Fail<Guid>(doctorResult.Errors);

        var procedureResult = await _procedureService.GetByIdAsync(new GetProcedureByIdQuery(currentAppointment.ProcedureId));

        if (!procedureResult.Success)
            return Result.Fail<Guid>(procedureResult.Errors);

        var duration = currentAppointment.ScheduledEndDate.Subtract(currentAppointment.ScheduledStartDate).TotalMinutes;

        var endDate = request.StartDate.AddMinutes(duration);

        var hasSchedule = doctorResult.Value.Schedules.Any(s => request.StartDate >= s.StartDate && endDate <= s.EndDate);

        if (!hasSchedule)
            return Result.Fail<Guid>(AppointmentErrors.DoctorHasNoSchedule);

        var isUnique = await _appointmentRepository
            .IsUniqueAsync(currentAppointment.PatientId, currentAppointment.DoctorId, request.StartDate, endDate, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(AppointmentErrors.IsNotUnique);

        var newAppointmentResult = currentAppointment.Reschedule(request.StartDate, endDate);

        if (!newAppointmentResult.Success)
            return Result.Fail<Guid>(newAppointmentResult.Errors);

        currentAppointment.Update(request.StartDate, endDate);

        _appointmentRepository.Update(currentAppointment);

        var newAppointment = newAppointmentResult.Value;

        _appointmentRepository.Create(newAppointment);

        var patientName = $"{patientResult.Value.FirstName} {patientResult.Value.LastName}";
        var doctorName = $"{doctorResult.Value.FirstName} {doctorResult.Value.LastName}";

        RaiseAppointmentUpdatedEvent(
            currentAppointment,
            patientName,
            patientResult.Value.Email,
            doctorName,
            doctorResult.Value.Email,
            procedureResult.Value.Name,
            request.StartDate,
            endDate);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail<Guid>(AppointmentErrors.CannotBeUpdated);

        return Result.Ok(newAppointment.Id);
    }

    private static void RaiseAppointmentUpdatedEvent(
        Appointment appointment,
        string patientName,
        string patientEmail,
        string doctorName,
        string doctorEmail,
        string procedureName,
        DateTime startDate,
        DateTime endDate)
    {
        var patientEvent = new PersonEvent(patientName, patientEmail);
        var doctorEvent = new PersonEvent(doctorName, doctorEmail);
        var procedureEvent = new ProcedureEvent(procedureName, startDate, endDate);

        var appointmentScheduledEvent =
            new AppointmentUpdatedEvent(appointment.Id, patientEvent, doctorEvent, procedureEvent);

        appointment.RaiseEvent(appointmentScheduledEvent);
    }
}