namespace MyClinic.Doctors.Application.Specialities.UpdateSpeciality;

public sealed record UpdateSpecialityInputModel(
    string Name);

public static class UpdateSpecialityInputModelExtension
{
    public static UpdateSpecialityCommand ToCommand(this UpdateSpecialityInputModel model, Guid id)
    {
        return new UpdateSpecialityCommand(
            id,
            model.Name);
    }
}