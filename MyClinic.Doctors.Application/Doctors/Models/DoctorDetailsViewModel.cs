using MyClinic.Common.ValueObjects;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Application.Doctors.Models;

public record DoctorDetailsViewModel(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Cpf,
    string Email,
    string Telephone,
    Address Address,
    BloodData BloodData,
    GenderType Gender,
    string LicenseNumber,
    ICollection<string>? Specialities,
    ICollection<DoctorSchedule>? Schedules);

public static class DoctorDetailsViewModelExtension
{
    public static DoctorDetailsViewModel ToDetailsViewModel(this Doctor doctor)
    {
        return new DoctorDetailsViewModel(
            doctor.Id,
            doctor.FirstName,
            doctor.LastName,
            doctor.BirthDate,
            doctor.Cpf.Number,
            doctor.Email.Address,
            doctor.Telephone.Number,
            doctor.Address,
            doctor.BloodData,
            doctor.Gender.Type,
            doctor.LicenseNumber,
            doctor.Specialities.Select(s => s.Name).ToList(),
            doctor.Schedules.ToDoctorSchedule());
    }

    public static ICollection<DoctorDetailsViewModel> ToDetailsViewModel(this IEnumerable<Doctor> doctors)
    {
        return doctors is not null
             ? doctors.Select(fg => fg.ToDetailsViewModel()).ToList()
             : [];
    }
}