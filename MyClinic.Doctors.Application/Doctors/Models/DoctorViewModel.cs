using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Application.Doctors.Models;

public record DoctorViewModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Telephone);

public static class DoctorViewModelExtension
{
    public static DoctorViewModel ToViewModel(this Doctor doctor)
    {
        return new DoctorViewModel(
            doctor.Id,
            doctor.FirstName,
            doctor.LastName,
            doctor.Email.Address,
            doctor.Telephone.Number);
    }

    public static IEnumerable<DoctorViewModel> ToViewModel(this IEnumerable<Doctor> doctors)
    {
        return doctors is not null
             ? doctors.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}