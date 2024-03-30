using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Insurances.Models;
using MyClinic.Patients.Application.Insurances.Services;
using MyClinic.Patients.Application.Insurances.GetInsurance;
using MyClinic.Patients.Application.Insurances.GetInsurances;
using MyClinic.Patients.Application.Insurances.CreateInsurance;

namespace MyClinic.API.Controllers.Modules.Patients;

[Route("api/v1/[controller]")]
[ApiController]
public class InsurancesController : ControllerBase
{
    private readonly IInsuranceService _insuranceService;

    public InsurancesController(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<InsuranceViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetInsurancesQuery query)
    {
        var result = await _insuranceService.GetAllAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InsuranceViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetInsuranceQuery(id);

        var result = await _insuranceService.GetByIdAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateInsuranceCommand command)
    {
        var result = await _insuranceService.CreateAsync(command);

        return result.Match(
        onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: value => value.ToProblemDetails());
    }
}