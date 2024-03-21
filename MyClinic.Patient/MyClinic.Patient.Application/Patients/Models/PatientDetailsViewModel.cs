using MyClinic.Common.ValueObjects;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Application.Insurances.Models;

namespace MyClinic.Patients.Application.Patients.Models;

public record PatientDetailsViewModel(
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

public static class PatientDetailsViewModelExtension
{
    public static PatientDetailsViewModel ToDetailsViewModel(this Patient patient)
    {
        return new PatientDetailsViewModel(
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
            patient.Insurance?.ToViewModel());
    }

    public static IEnumerable<PatientDetailsViewModel> ToDetailsViewModel(this IEnumerable<Patient> patients)
    {
        return patients is not null
             ? patients.Select(fg => fg.ToDetailsViewModel()).ToList()
             : [];
    }
}