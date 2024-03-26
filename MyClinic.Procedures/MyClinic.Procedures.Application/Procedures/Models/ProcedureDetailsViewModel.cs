using MyClinic.Procedures.Domain.Entities;

namespace MyClinic.Procedures.Application.Procedures.Models;

public record ProcedureDetailsViewModel(
    Guid Id,
    string Name,
    string Description,
    decimal Cost,
    int Duration,
    int MinimumSchedulingNotice,
    ProcedureSpecialityViewModel Speciality);

public static class ProcedureDetailsViewModelExtension
{
    public static ProcedureDetailsViewModel ToDetailsViewModel(this Procedure procedure, ProcedureSpecialityViewModel specialityModel)
    {
        return new ProcedureDetailsViewModel(
            procedure.Id,
            procedure.Name,
            procedure.Description,
            procedure.Cost,
            procedure.Duration,
            procedure.MinimumSchedulingNotice,
            specialityModel);
    }
}