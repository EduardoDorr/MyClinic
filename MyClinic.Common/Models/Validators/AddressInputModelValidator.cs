using FluentValidation;

using MyClinic.Common.Models.InputModels;

namespace MyClinic.Common.Models.Validators;

public sealed class AddressInputModelValidator : AbstractValidator<AddressInputModel>
{
    public AddressInputModelValidator()
    {
        RuleFor(r => r.Street)
            .MinimumLength(5).WithMessage("Street must have a minimum of 5 characters")
            .MaximumLength(100).WithMessage("Street must have a maximum of 100 characters");

        RuleFor(r => r.City)
            .MinimumLength(5).WithMessage("City must have a minimum of 5 characters")
            .MaximumLength(50).WithMessage("City must have a maximum of 50 characters");

        RuleFor(r => r.State)
            .MinimumLength(5).WithMessage("State must have a minimum of 5 characters")
            .MaximumLength(50).WithMessage("State must have a maximum of 50 characters");

        RuleFor(r => r.Country)
            .MinimumLength(5).WithMessage("Country must have a minimum of 5 characters")
            .MaximumLength(50).WithMessage("Country must have a maximum of 50 characters");

        RuleFor(r => r.ZipCode)
            .MinimumLength(8).WithMessage("ZipCode must have a minimum of 8 characters")
            .MaximumLength(9).WithMessage("ZipCode must have a maximum of 9 characters");
    }
}