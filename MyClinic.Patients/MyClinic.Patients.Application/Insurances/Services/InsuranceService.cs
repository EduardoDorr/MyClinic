using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Insurances.Models;
using MyClinic.Patients.Application.Insurances.GetInsurance;
using MyClinic.Patients.Application.Insurances.GetInsurances;
using MyClinic.Patients.Application.Insurances.CreateInsurance;

namespace MyClinic.Patients.Application.Insurances.Services;

public sealed class InsuranceService : IInsuranceService
{
    private readonly IMediator _mediator;

    public InsuranceService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<PaginationResult<InsuranceViewModel>>> GetAllAsync(GetInsurancesQuery query) =>
        await _mediator.Send(query);

    public async Task<Result<InsuranceViewModel?>> GetByIdAsync(GetInsuranceQuery query) =>
        await _mediator.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateInsuranceCommand command) =>
        await _mediator.Send(command);
}