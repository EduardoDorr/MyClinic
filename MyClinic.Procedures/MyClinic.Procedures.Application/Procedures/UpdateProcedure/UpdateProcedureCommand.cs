using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Procedures.Application.Procedures.UpdateProcedure;

public sealed record UpdateProcedureCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Cost,
    int Duration,
    int MinimumSchedulingNotice,
    Guid SpecialityId) : IRequest<Result>;