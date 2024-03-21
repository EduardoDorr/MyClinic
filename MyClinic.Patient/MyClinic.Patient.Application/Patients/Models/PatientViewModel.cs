using MyClinic.Common.ValueObjects;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Insurances.Models;

namespace MyClinic.Patients.Application.Patients.Models;

public record PatientViewModel(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Telephone);

public static class PatientViewModelExtension
{
    public static PatientViewModel ToViewModel(this Patient patient)
    {
        return new PatientViewModel(
            patient.Id,
            patient.FirstName,
            patient.LastName,
            patient.Email.Address,
            patient.Telephone.Number);
    }

    public static IEnumerable<PatientViewModel> ToViewModel(this IEnumerable<Patient> patients)
    {
        return patients is not null
             ? patients.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}