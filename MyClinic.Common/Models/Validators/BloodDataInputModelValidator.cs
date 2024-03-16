using FluentValidation;

using MyClinic.Common.Models.InputModels;

namespace MyClinic.Common.Models.Validators;

public sealed class BloodDataInputModelValidator : AbstractValidator<BloodDataInputModel>
{
    public BloodDataInputModelValidator()
    {
        RuleFor(r => r.BloodType)
            .IsInEnum().WithMessage("BloodType must match the specific types");

        RuleFor(r => r.RhFactor)
            .IsInEnum().WithMessage("RhFactor must match the specific types");
    }
}