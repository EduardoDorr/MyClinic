using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Procedures.Application.Procedures.DeleteProcedure;

public sealed record DeleteProcedureCommand(Guid Id) : IRequest<Result>;