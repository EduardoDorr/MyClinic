using MyClinic.Common.ValueObjects;
using MyClinic.Common.Models.InputModels;

namespace MyClinic.Patients.Application.Patients.UpdatePatient;

public sealed record UpdatePatientInputModel(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Email,
    string Telephone,
    AddressInputModel Address,
    BloodDataInputModel BloodData,
    GenderType Gender,
    int Height,
    decimal Weight,
    Guid? InsuranceId);

public static class UpdatePatientInputModelExtension
{
    public static UpdatePatientCommand ToCommand(this UpdatePatientInputModel model, Guid id)
    {
        return new UpdatePatientCommand(
            id,
            model.FirstName,
            model.LastName,
            model.BirthDate,
            model.Email,
            model.Telephone,
            model.Address,
            model.BloodData,
            model.Gender,
            model.Height,
            model.Weight,
            model.InsuranceId);
    }
}