using MediatR;

using MyClinic.Common.Results;
using MyClinic.Procedures.Application.Procedures.Models;

namespace MyClinic.Procedures.Application.Procedures.GetProcedureById;

public sealed record GetProcedureByIdQuery(Guid Id) : IRequest<Result<ProcedureDetailsViewModel?>>;