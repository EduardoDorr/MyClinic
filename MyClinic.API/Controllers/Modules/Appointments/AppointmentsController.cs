using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Application.Appointments.Models;
using MyClinic.Appointments.Application.Appointments.Services;
using MyClinic.Appointments.Application.Appointments.GetAppointments;
using MyClinic.Appointments.Application.Appointments.GetAppointmentById;
using MyClinic.Appointments.Application.Appointments.GetAppointmentsByDoctor;
using MyClinic.Appointments.Application.Appointments.GetAppointmentsByPatient;
using MyClinic.Appointments.Application.Appointments.CreateAppointment;
using MyClinic.Appointments.Application.Appointments.UpdateAppointment;
using MyClinic.Appointments.Application.Appointments.DeleteAppointment;

namespace MyClinic.API.Controllers.Modules.Appointments
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<AppointmentViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetAppointmentsQuery query)
        {
            var result = await _appointmentService.GetAllAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }

        [HttpGet("get-all/patient/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<AppointmentViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByPatient(Guid id, [FromQuery] GetAppointmentsQuery model)
        {
            var query = new GetAppointmentsByPatientQuery(id, model.Page, model.PageSize);

            var result = await _appointmentService.GetAllByPatientAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }

        [HttpGet("get-all/doctor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<AppointmentViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByDoctor(Guid id, [FromQuery] GetAppointmentsQuery model)
        {
            var query = new GetAppointmentsByDoctorQuery(id, model.Page, model.PageSize);

            var result = await _appointmentService.GetAllByDoctorAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentDetailsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAppointmentByIdQuery(id);

            var result = await _appointmentService.GetByIdAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
        {
            var result = await _appointmentService.CreateAsync(command);

            return result.Match(
            onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
            onFailure: value => value.ToProblemDetails());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentInputModel model)
        {
            var result = await _appointmentService.UpdateAsync(id, model);

            return result.Match(
            onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, model),
            onFailure: value => value.ToProblemDetails());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _appointmentService.DeleteAsync(new DeleteAppointmentCommand(id));

            return result.Match(
            onSuccess: NoContent,
            onFailure: value => value.ToProblemDetails());
        }
    }
}