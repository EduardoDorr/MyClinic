using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Domain.Interfaces;
using MyClinic.Patients.Domain.Entities.Insurances;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Insurances.Models;

namespace MyClinic.Patients.Application.Insurances.GetInsurance;

public sealed class GetInsuranceQueryHandler : IRequestHandler<GetInsuranceQuery, Result<InsuranceViewModel?>>
{
    private readonly IInsuranceRepository _insuranceRepository;

    public GetInsuranceQueryHandler(IInsuranceRepository insuranceRepository)
    {
        _insuranceRepository = insuranceRepository;
    }

    public async Task<Result<InsuranceViewModel?>> Handle(GetInsuranceQuery request, CancellationToken cancellationToken)
    {
        var insurance = await _insuranceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (insurance is null)
            return Result.Fail<InsuranceViewModel?>(InsuranceErrors.NotFound);

        var insuranceViewModel = insurance?.ToViewModel();

        return Result.Ok(insuranceViewModel);
    }
}