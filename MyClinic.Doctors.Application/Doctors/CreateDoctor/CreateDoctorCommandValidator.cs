using FluentValidation;

using MyClinic.Common.Models.Validators;

namespace MyClinic.Doctors.Application.Doctors.CreateDoctor;

public sealed class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .MinimumLength(3).WithMessage("First Name must have a minimum of 3 characters")
            .MaximumLength(50).WithMessage("First Name must have a maximum of 50 characters");

        RuleFor(r => r.LastName)
            .MinimumLength(3).WithMessage("Last Name must have a minimum of 3 characters")
            .MaximumLength(100).WithMessage("Last Name must have a maximum of 100 characters");

        RuleFor(r => r.BirthDate)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Birth Date must be at most today");

        RuleFor(r => r.Cpf)
            .MinimumLength(11).WithMessage("CPF must have a minimum of 11 characters")
            .MaximumLength(14).WithMessage("CPF must have a maximum of 14 characters");

        RuleFor(r => r.Email)
            .MinimumLength(5).WithMessage("Email must have a minimum of 5 characters")
            .MaximumLength(100).WithMessage("Email must have a maximum of 100 characters");

        RuleFor(r => r.Telephone)
            .MinimumLength(10).WithMessage("Telephone must have a minimum of 10 characters")
            .MaximumLength(16).WithMessage("Telephone must have a maximum of 16 characters");

        RuleFor(r => r.Address)
            .SetValidator(new AddressInputModelValidator());

        RuleFor(r => r.BloodData)
            .SetValidator(new BloodDataInputModelValidator());

        RuleFor(r => r.Gender)
            .IsInEnum().WithMessage("Gender must match the specific types");

        RuleFor(r => r.LicenseNumber)
            .MinimumLength(7).WithMessage("License Number must have a minimum of 7 characters")
            .MaximumLength(12).WithMessage("License Number must have a maximum of 12 characters");

        RuleForEach(r => r.SpecialitiesId)
           .Must(r => Guid.TryParse(r.ToString(), out Guid result))
           .When(r => r.SpecialitiesId is not null)
           .WithMessage("Speciality is not valid");

        RuleForEach(r => r.Schedules)
           .Must(r => r.StartDate.CompareTo(r.EndDate) < 0)
           .When(r => r.Schedules is not null)
           .WithMessage("Start date must be earlier than end date");
    }
}