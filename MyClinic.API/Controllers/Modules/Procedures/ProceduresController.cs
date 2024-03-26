using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Application.Procedures.Models;
using MyClinic.Procedures.Application.Procedures.Services;
using MyClinic.Procedures.Application.Procedures.GetProcedures;
using MyClinic.Procedures.Application.Procedures.GetProcedureById;
using MyClinic.Procedures.Application.Procedures.GetProceduresBySpeciality;
using MyClinic.Procedures.Application.Procedures.CreateProcedure;
using MyClinic.Procedures.Application.Procedures.UpdateProcedure;
using MyClinic.Procedures.Application.Procedures.DeleteProcedure;

namespace MyClinic.API.Controllers.Modules.Procedures;

[Route("api/[controller]")]
[ApiController]
public class ProceduresController : ControllerBase
{
    private readonly IProcedureService _procedureService;

    public ProceduresController(IProcedureService procedureService)
    {
        _procedureService = procedureService;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<ProcedureViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetProceduresQuery query)
    {
        var result = await _procedureService.GetAllAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("get-all/speciality/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<ProcedureViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBySpeciality(Guid id, [FromQuery] GetProceduresQuery model)
    {
        var query = new GetProceduresBySpecialityQuery(id, model.Page, model.PageSize);

        var result = await _procedureService.GetAllBySpecialityAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProcedureDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetProcedureByIdQuery(id);

        var result = await _procedureService.GetByIdAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateProcedureCommand command)
    {
        var result = await _procedureService.CreateAsync(command);

        return result.Match(
        onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProcedureInputModel model)
    {
        var result = await _procedureService.UpdateAsync(id, model);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _procedureService.DeleteAsync(new DeleteProcedureCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}