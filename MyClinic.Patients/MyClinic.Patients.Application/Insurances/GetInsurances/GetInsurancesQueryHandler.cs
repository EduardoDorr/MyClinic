using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Insurances.Models;
using MyClinic.Patients.Domain.Repositories;

namespace MyClinic.Patients.Application.Insurances.GetInsurances;

public sealed class GetInsurancesQueryHandler : IRequestHandler<GetInsurancesQuery, Result<PaginationResult<InsuranceViewModel>>>
{
    private readonly IInsuranceRepository _insuranceRepository;

    public GetInsurancesQueryHandler(IInsuranceRepository insuranceRepository)
    {
        _insuranceRepository = insuranceRepository;
    }

    public async Task<Result<PaginationResult<InsuranceViewModel?>>> Handle(GetInsurancesQuery request, CancellationToken cancellationToken)
    {
        var paginationInsurances = await _insuranceRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var insurancesViewModel = paginationInsurances.Data.ToViewModel();

        var paginationInsurancesViewModel =
            new PaginationResult<InsuranceViewModel?>
            (
                paginationInsurances.Page,
                paginationInsurances.PageSize,
                paginationInsurances.TotalCount,
                paginationInsurances.TotalPages,
                insurancesViewModel.ToList()
            );

        return Result.Ok(paginationInsurancesViewModel);
    }
}