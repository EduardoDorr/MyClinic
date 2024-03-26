using FluentValidation;

namespace MyClinic.Procedures.Application.Procedures.CreateProcedure;

public sealed class CreateProcedureCommandValidator : AbstractValidator<CreateProcedureCommand>
{
    public CreateProcedureCommandValidator()
    {
        RuleFor(r => r.Name)
            .MinimumLength(3).WithMessage("Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("Name must have a maximum of 50 characters");

        RuleFor(r => r.Description)
            .MinimumLength(5).WithMessage("Description must have a minimum of 5 characters")
            .MaximumLength(255).WithMessage("Description must have a maximum of 100 characters");

        RuleFor(r => r.Cost)
            .GreaterThan(0).WithMessage("Cost is not valid");

        RuleFor(r => r.Duration)
            .GreaterThan(0).WithMessage("Duration is not valid");

        RuleFor(r => r.MinimumSchedulingNotice)
            .GreaterThanOrEqualTo(0).WithMessage("minum Scheduling Notice is not valid");

        RuleFor(r => r.SpecialityId)
           .Must(r => Guid.TryParse(r.ToString(), out Guid result))
           .WithMessage("Speciality Id is not valid");
    }
}