using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Specialities.Models;

public record SpecialityDetailsViewModel(
    Guid Id,
    string Name,
    ICollection<DoctorViewModel> Doctors);

public static class SpecialityDetailsViewModelExtension
{
    public static SpecialityDetailsViewModel ToDetailsViewModel(this Speciality speciality)
    {
        return new SpecialityDetailsViewModel(
            speciality.Id,
            speciality.Name,
            speciality.Doctors.ToViewModel());
    }

    public static ICollection<SpecialityDetailsViewModel> ToDetailsViewModel(this IEnumerable<Speciality> specialities)
    {
        return specialities is not null
             ? specialities.Select(fg => fg.ToDetailsViewModel()).ToList()
             : [];
    }
}