using FluentValidation;

namespace MyClinic.Appointments.Application.Appointments.UpdateAppointment;

public sealed class UpdateAppointmentInputModelValidator : AbstractValidator<UpdateAppointmentInputModel>
{
    public UpdateAppointmentInputModelValidator()
    {
        RuleFor(r => r.StartDate)
            .Must(r => r.CompareTo(DateTime.Now) > 0)
            .WithMessage("Start date must be later than now");
    }
}