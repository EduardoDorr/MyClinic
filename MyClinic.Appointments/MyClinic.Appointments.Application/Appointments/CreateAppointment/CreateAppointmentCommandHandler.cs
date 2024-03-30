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


namespace MyClinic.Appointments.Application.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IProcedureService _procedureService;

    public CreateAppointmentCommandHandler(
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

    public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var patientResult = await _patientService.GetByIdAsync(new GetPatientByIdQuery(request.PatientId));

        if (!patientResult.Success)
            return Result.Fail<Guid>(patientResult.Errors);

        var doctorResult = await _doctorService.GetByIdAsync(new GetDoctorByIdQuery(request.DoctorId));

        if (!doctorResult.Success)
            return Result.Fail<Guid>(doctorResult.Errors);

        var procedureResult = await _procedureService.GetByIdAsync(new GetProcedureByIdQuery(request.ProcedureId));

        if (!procedureResult.Success)
            return Result.Fail<Guid>(procedureResult.Errors);

        var hasSpeciality = doctorResult.Value.Specialities.Any(s => s == procedureResult.Value.Speciality.Name);

        if (!hasSpeciality)
            return Result.Fail<Guid>(AppointmentErrors.DoctorDoesNotHaveSpeciality);

        var minimumSchedulingDate = DateTime.Now.AddDays(procedureResult.Value.MinimumSchedulingNotice);

        if (minimumSchedulingDate.CompareTo(request.StartDate) > 0)
            return Result.Fail<Guid>(AppointmentErrors.MinimumSchedulingNotice);

        var endDate = request.StartDate.AddMinutes(procedureResult.Value.Duration);

        var hasSchedule = doctorResult.Value.Schedules.Any(s => request.StartDate >= s.StartDate && endDate <= s.EndDate);

        if (!hasSchedule)
            return Result.Fail<Guid>(AppointmentErrors.DoctorHasNoSchedule);

        var isUnique = await _appointmentRepository
            .IsUniqueAsync(request.PatientId, request.DoctorId, request.StartDate, endDate, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(AppointmentErrors.IsNotUnique);

        var cost = GetDiscount(patientResult.Value?.Insurance?.BasicDiscount) * procedureResult.Value.Cost;

        var appointmentResult = Appointment.Create
            (
                request.PatientId,
                request.DoctorId,
                request.ProcedureId,
                cost,
                request.StartDate,
                endDate,
                request.Type
            );

        if (!appointmentResult.Success)
            return Result.Fail<Guid>(appointmentResult.Errors);

        var appointment = appointmentResult.Value;

        _appointmentRepository.Create(appointment);

        var patientName = $"{patientResult.Value.FirstName} {patientResult.Value.LastName}";
        var doctorName = $"{doctorResult.Value.FirstName} {doctorResult.Value.LastName}";

        RaiseAppointmentCreatedEvent(
            appointment,
            patientName,
            patientResult.Value.Email,
            doctorName,
            doctorResult.Value.Email,
            procedureResult.Value.Name,
            request.StartDate,
            endDate);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(AppointmentErrors.CannotBeCreated);

        return Result.Ok(appointment.Id);
    }

    private static decimal GetDiscount(decimal? discount)
    {
        if (discount is null) 
            return 1.0M;

        return (100 - discount.Value) / 100M;
    }

    private static void RaiseAppointmentCreatedEvent(
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
            new AppointmentCreatedEvent(appointment.Id, patientEvent, doctorEvent, procedureEvent);

        appointment.RaiseEvent(appointmentScheduledEvent);
    }
}