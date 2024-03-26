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
    string LicenseNumber);

public static class UpdateDoctorInputModelExtension
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
            model.LicenseNumber);
    }
}