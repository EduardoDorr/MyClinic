using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Application.Specialities.Models;

public record SpecialityViewModel(
    Guid Id,
    string Name);

public static class SpecialityViewModelExtension
{
    public static SpecialityViewModel ToViewModel(this Speciality speciality)
    {
        return new SpecialityViewModel(
            speciality.Id,
            speciality.Name);
    }

    public static IEnumerable<SpecialityViewModel> ToViewModel(this IEnumerable<Speciality> specialities)
    {
        return specialities is not null
             ? specialities.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}