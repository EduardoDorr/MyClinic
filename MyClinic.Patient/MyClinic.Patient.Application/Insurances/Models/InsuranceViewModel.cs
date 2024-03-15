using MyClinic.Patients.Domain.Entities.Insurances;

namespace MyClinic.Patients.Application.Insurances.Models;

public record InsuranceViewModel(
    Guid Id,
    string Name,
    decimal BasicDiscount);

public static class InsuranceViewModelExtension
{
    public static InsuranceViewModel ToViewModel(this Insurance insurance)
    {
        return new InsuranceViewModel(
            insurance.Id,
            insurance.Name,
            insurance.BasicDiscount);
    }

    public static IEnumerable<InsuranceViewModel> ToViewModel(this IEnumerable<Insurance> insurances)
    {
        return insurances is not null
             ? insurances.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}