using Microsoft.AspNetCore.Mvc;

using MyClinic.API.Extensions;
using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.Services;
using MyClinic.Patients.Application.Patients.GetPatient;
using MyClinic.Patients.Application.Patients.GetPatients;
using MyClinic.Patients.Application.Patients.CreatePatient;
using MyClinic.Patients.Application.Patients.UpdatePatient;
using MyClinic.Patients.Application.Patients.DeletePatient;

namespace MyClinic.API.Controllers.Modules.Patients
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResult<PatientViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetPatientsQuery query)
        {
            var result = await _patientService.GetAllAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPatientQuery(id);

            var result = await _patientService.GetByIdAsync(query);

            return result.Match(
            onSuccess: Ok,
            onFailure: value => value.ToProblemDetails());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
        {
            var result = await _patientService.CreateAsync(command);

            return result.Match(
            onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
            onFailure: value => value.ToProblemDetails());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientInputModel model)
        {
            var result = await _patientService.UpdateAsync(id, model);

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
            var result = await _patientService.Delete(new DeletePatientCommand(id));

            return result.Match(
            onSuccess: NoContent,
            onFailure: value => value.ToProblemDetails());
        }
    }
}