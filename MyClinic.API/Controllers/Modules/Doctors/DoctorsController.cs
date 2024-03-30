using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Application.Doctors.Services;
using MyClinic.Doctors.Application.Doctors.GetDoctors;
using MyClinic.Doctors.Application.Doctors.GetDoctorBy;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Doctors.Application.Doctors.GetDoctorsBySpeciality;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;
using MyClinic.Doctors.Application.Doctors.UpdateDoctor;
using MyClinic.Doctors.Application.Doctors.DeleteDoctor;

namespace MyClinic.API.Controllers.Modules.Doctors;

[Route("api/v1/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<DoctorViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetDoctorsQuery query)
    {
        var result = await _doctorService.GetAllAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("get-all/speciality/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<DoctorViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBySpeciality(Guid id, [FromQuery] GetDoctorsQuery model)
    {
        var query = new GetDoctorsBySpecialityQuery(id, model.Page, model.PageSize);

        var result = await _doctorService.GetAllBySpecialityAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetDoctorByIdQuery(id);

        var result = await _doctorService.GetByIdAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorDetailsViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBy([FromQuery] GetDoctorByQuery query)
    {
        var result = await _doctorService.GetByAsync(query);

        return result.Match(
        onSuccess: Ok,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorCommand command)
    {
        var result = await _doctorService.CreateAsync(command);

        return result.Match(
        onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDoctorInputModel model)
    {
        var result = await _doctorService.UpdateAsync(id, model);

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
        var result = await _doctorService.DeleteAsync(new DeleteDoctorCommand(id));

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/add-specialities")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddSpecialities(Guid id, [FromBody] List<Guid> specialitiesId)
    {
        var result = await _doctorService.AddSpecialitiesAsync(id, specialitiesId);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/remove-specialities")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveSpecialities(Guid id, [FromBody] List<Guid> specialitiesId)
    {
        var result = await _doctorService.RemoveSpecialitiesAsync(id, specialitiesId);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/add-schedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddSchedules(Guid id, [FromBody] List<DoctorSchedule> doctorSchedules)
    {
        var result = await _doctorService.AddSchedulesAsync(id, doctorSchedules);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }

    [HttpPut("{id}/remove-schedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveSchedules(Guid id, [FromBody] List<Guid> schedulesId)
    {
        var result = await _doctorService.RemoveSchedulesAsync(id, schedulesId);

        return result.Match(
        onSuccess: NoContent,
        onFailure: value => value.ToProblemDetails());
    }
}