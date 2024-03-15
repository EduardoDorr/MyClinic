using MyClinic.Common.ValueObjects;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Insurances.Models;

namespace MyClinic.Patients.Application.Patients.Models;

public record PatientViewModel(
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
    int Height,
    decimal Weight,
    InsuranceViewModel Insurance);

public static class PatientViewModelExtension
{
    public static PatientViewModel ToViewModel(this Patient patient)
    {
        return new PatientViewModel(
            patient.Id,
            patient.FirstName,
            patient.LastName,
            patient.BirthDate,
            patient.Cpf.Number,
            patient.Email.Address,
            patient.Telephone.Number,
            patient.Address,
            patient.BloodData,
            patient.Gender.Type,
            patient.Height,
            patient.Weight,
            patient.Insurance.ToViewModel());
    }

    public static IEnumerable<PatientViewModel> ToViewModel(this IEnumerable<Patient> patients)
    {
        return patients is not null
             ? patients.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}