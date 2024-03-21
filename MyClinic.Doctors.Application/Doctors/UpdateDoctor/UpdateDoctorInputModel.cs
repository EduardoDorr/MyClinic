using MyClinic.Common.ValueObjects;
using MyClinic.Common.Models.InputModels;

namespace MyClinic.Doctors.Application.Doctors.UpdateDoctor;

public sealed record UpdateDoctorInputModel(
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
    public static UpdateDoctorCommand ToCommand(this UpdateDoctorInputModel model, Guid id)
    {
        return new UpdateDoctorCommand(
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