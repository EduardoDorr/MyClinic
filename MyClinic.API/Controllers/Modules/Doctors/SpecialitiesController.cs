using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Specialities.Models;
using MyClinic.Doctors.Application.Specialities.Services;
using MyClinic.Doctors.Application.Specialities.GetSpeciality;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;
using MyClinic.Doctors.Application.Specialities.DeleteSpeciality;
using MyClinic.Doctors.Application.Specialities.UpdateSpeciality;
using MyClinic.Doctors.Application.Specialities.CreateSpeciality;

namespace MyClinic.API.Controllers.Modules.Doctors;

[Route("api/v1/[controller]")]
[ApiController]
public class SpecialitesController : ControllerBase
{
    private readonly ISpecialityService _specialityService;

    public SpecialitesController(ISpecialityService specialityService)
    {
        _specialityService = specialityService;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<SpecialityViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetSpecialitiesQuery query)
    {
        var result = await _specialityService.GetAllAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SpecialityDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetSpecialityByIdQuery(id);

        var result = await _specialityService.GetByIdAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateSpecialityCommand command)
    {
        var result = await _specialityService.CreateAsync(command);

        return result.Match(
        onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSpecialityInputModel model)
    {
        var result = await _specialityService.UpdateAsync(id, model);

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
        var result = await _specialityService.DeleteAsync(new DeleteSpecialityCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}