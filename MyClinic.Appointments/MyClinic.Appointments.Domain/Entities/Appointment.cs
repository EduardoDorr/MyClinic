using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Common.DomainEvents;

namespace MyClinic.Appointments.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid ProcedureId { get; private set; }
    public DateTime ScheduledStartDate { get; private set; }
    public DateTime ScheduledEndDate { get; private set; }
    public DateTime? RealStartDate { get; private set; }
    public DateTime? RealEndDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }
    public AppointmentType Type { get; private set; }
    public AppointmentStatus Status { get; private set; }

    protected Appointment() { }

    private Appointment(
        Guid patientId,
        Guid doctorId,
        Guid procedureId,
        DateTime startDate,
        DateTime endDate,
        AppointmentType type)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        ProcedureId = procedureId;
        ScheduledStartDate = startDate;
        ScheduledEndDate = endDate;
        Type = type;

        Status = AppointmentStatus.Scheduled;
    }

    public static Result<Appointment> Create(
        Guid patientId,
        Guid doctorId,
        Guid procedureId,
        DateTime scheduledStartDate,
        DateTime scheduledEndDate,
        AppointmentType type)
    {
        var appointment =
            new Appointment(
                patientId,
                doctorId,
                procedureId,
                scheduledStartDate,
                scheduledEndDate,
                type);

        return Result.Ok(appointment);
    }

    public void Update(
        DateTime scheduledStartDate,
        DateTime scheduledEndDate)
    {
        ScheduledStartDate = scheduledStartDate;
        ScheduledEndDate = scheduledEndDate;
    }

    private void SetCancellationDate() =>
        CancellationDate = DateTime.Now;

    private void SetRealStartDate(DateTime realStartDate) =>
        RealStartDate = realStartDate;

    private void SetRealEndDate(DateTime realEndDate) =>
        RealEndDate = realEndDate;

    private void SetStatus(AppointmentStatus status) =>
       Status = status;

    public Result SetPending()
    {
        if (Status is not AppointmentStatus.Scheduled)
            return Result.Fail(AppointmentErrors.StatusDoesNotMatchMethod);

        SetStatus(AppointmentStatus.Pending);

        return Result.Ok();
    }

    public Result Confirm()
    {
        if (Status is not AppointmentStatus.Pending)
            return Result.Fail(AppointmentErrors.StatusDoesNotMatchMethod);

        SetStatus(AppointmentStatus.Confirmed);

        return Result.Ok();
    }

    public Result Start(DateTime startDate)
    {
        if (Status is not AppointmentStatus.Confirmed)
            return Result.Fail(AppointmentErrors.StatusDoesNotMatchMethod);

        SetStatus(AppointmentStatus.InProgress);
        SetRealStartDate(startDate);

        return Result.Ok();
    }

    public Result Complete(DateTime endDate)
    {
        if (Status is not AppointmentStatus.InProgress)
            return Result.Fail(AppointmentErrors.StatusDoesNotMatchMethod);

        if (endDate.CompareTo(RealStartDate) <= 0)
            return Result.Fail(AppointmentErrors.EndDateIsInvalid);

        SetStatus(AppointmentStatus.Completed);
        SetRealEndDate(endDate);

        return Result.Ok();
    }

    public Result NotAttended()
    {
        if (Status is not (AppointmentStatus.Pending or AppointmentStatus.Confirmed))
            return Result.Fail(AppointmentErrors.StatusDoesNotMatchMethod);

        SetStatus(AppointmentStatus.NotAttended);

        return Result.Ok();
    }

    public Result<Appointment> Reschedule(DateTime newStartDate, DateTime newEndDate)
    {
        if (Status is (AppointmentStatus.CanceledByPatient or AppointmentStatus.CanceledByDoctor))
            return Result.Fail<Appointment>(AppointmentErrors.AlreadyCanceled);

        if (Status is not (AppointmentStatus.Scheduled or AppointmentStatus.Pending or AppointmentStatus.Confirmed))
            return Result.Fail<Appointment>(AppointmentErrors.CannotBeRescheduled);

        SetStatus(AppointmentStatus.Rescheduled);
        SetCancellationDate();

        var appointment =
            new Appointment(
                PatientId,
                DoctorId,
                ProcedureId,
                newStartDate,
                newEndDate,
                Type);

        return Result.Ok(appointment);
    }

    public Result Cancel(bool byDoctor = false)
    {
        if (Status is (AppointmentStatus.CanceledByPatient or AppointmentStatus.CanceledByDoctor))
            return Result.Fail(AppointmentErrors.AlreadyCanceled);

        if (Status is not (AppointmentStatus.Scheduled or AppointmentStatus.Pending or AppointmentStatus.Confirmed))
            return Result.Fail(AppointmentErrors.CannotBeCanceled);

        var status = byDoctor ? AppointmentStatus.CanceledByDoctor : AppointmentStatus.CanceledByPatient;

        SetStatus(status);
        SetCancellationDate();

        return Result.Ok();
    }

    public void RaiseEvent(IDomainEvent domainEvent) =>
       RaiseDomainEvent(domainEvent);
}