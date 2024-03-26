using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Application.Procedures.Models;

namespace MyClinic.Procedures.Application.Procedures.GetProceduresBySpeciality;

public sealed record GetProceduresBySpecialityQuery(Guid SpecialityId, int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<ProcedureViewModel>>>;