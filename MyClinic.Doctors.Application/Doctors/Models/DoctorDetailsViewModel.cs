using MyClinic.Common.ValueObjects;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Entities.Schedules;
using MyClinic.Doctors.Domain.Entities.Specialities;

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
    ICollection<Speciality>? Specialities,
    ICollection<Schedule>? Schedules);

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
            doctor.Specialities,
            doctor.Schedules);
    }

    public static IEnumerable<DoctorDetailsViewModel> ToDetailsViewModel(this IEnumerable<Doctor> patients)
    {
        return patients is not null
             ? patients.Select(fg => fg.ToDetailsViewModel()).ToList()
             : [];
    }
}