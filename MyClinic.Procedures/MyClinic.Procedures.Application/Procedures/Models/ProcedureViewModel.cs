using MyClinic.Procedures.Domain.Entities;

namespace MyClinic.Procedures.Application.Procedures.Models;

public record ProcedureViewModel(
    Guid Id,
    string Name,
    string Description);

public static class ProcedureViewModelExtension
{
    public static ProcedureViewModel ToViewModel(this Procedure procedure)
    {
        return new ProcedureViewModel(
            procedure.Id,
            procedure.Name,
            procedure.Description);
    }

    public static ICollection<ProcedureViewModel> ToViewModel(this IEnumerable<Procedure> procedures)
    {
        return procedures is not null
             ? procedures.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}