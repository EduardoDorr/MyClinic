using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Application.Procedures.Models;
using MyClinic.Procedures.Domain.Repositories;

namespace MyClinic.Procedures.Application.Procedures.GetProcedures;

public sealed class GetProceduresQueryHandler : IRequestHandler<GetProceduresQuery, Result<PaginationResult<ProcedureViewModel>>>
{
    private readonly IProcedureRepository _procedureRepository;

    public GetProceduresQueryHandler(IProcedureRepository procedureRepository)
    {
        _procedureRepository = procedureRepository;
    }

    public async Task<Result<PaginationResult<ProcedureViewModel>>> Handle(GetProceduresQuery request, CancellationToken cancellationToken)
    {
        var paginationProcedures = await _procedureRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var proceduresViewModel = paginationProcedures.Data.ToViewModel();

        var paginationProceduresViewModel =
            new PaginationResult<ProcedureViewModel>
            (
                paginationProcedures.Page,
                paginationProcedures.PageSize,
                paginationProcedures.TotalCount,
                paginationProcedures.TotalPages,
                proceduresViewModel.ToList()
            );

        return Result.Ok(paginationProceduresViewModel);
    }
}