using FluentValidation;

namespace MyClinic.Appointments.Application.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(r => r.PatientId)
           .Must(r => Guid.TryParse(r.ToString(), out Guid result))
           .WithMessage("PatientId is not valid");

        RuleFor(r => r.DoctorId)
           .Must(r => Guid.TryParse(r.ToString(), out Guid result))
           .WithMessage("DoctorId is not valid");

        RuleFor(r => r.ProcedureId)
           .Must(r => Guid.TryParse(r.ToString(), out Guid result))
           .WithMessage("ProcedureId is not valid");        

        RuleFor(r => r.StartDate)
            .Must(r => r.CompareTo(DateTime.Now) > 0)
           .WithMessage("Start date must be later than now");
    }
}