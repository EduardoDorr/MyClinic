namespace MyClinic.Procedures.Application.Procedures.UpdateProcedure;

public sealed record UpdateProcedureInputModel(
    string Name,
    string Description,
    decimal Cost,
    int Duration,
    int MinimumSchedulingNotice,
    Guid SpecialityId);

public static class UpdatePatientInputModelExtension
{
    public static UpdateProcedureCommand ToCommand(this UpdateProcedureInputModel model, Guid id)
    {
        return new UpdateProcedureCommand(
            id,
            model.Name,
            model.Description,
            model.Cost,
            model.Duration,
            model.MinimumSchedulingNotice,
            model.SpecialityId);
    }
}