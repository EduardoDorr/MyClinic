using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Application.Procedures.Models;

namespace MyClinic.Procedures.Application.Procedures.GetProcedures;

public sealed record GetProceduresQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<ProcedureViewModel>>>;