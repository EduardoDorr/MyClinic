using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Procedures.Application.Procedures.CreateProcedure;

public record CreateProcedureCommand(
    string Name,
    string Description,
    decimal Cost,
    int Duration,
    int MinimumSchedulingNotice,
    Guid SpecialityId) : IRequest<Result<Guid>>;