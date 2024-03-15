using Microsoft.AspNetCore.Mvc;

using MediatR;

using MyClinic.Common.Results;
using MyClinic.API.Extensions;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.GetPatient;
using MyClinic.Patients.Application.Patients.CreatePatient;

namespace MyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPatientQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
            onSuccess: (value) => (IActionResult)Results.Ok(value),
            onFailure: value => value.ToProblemDetails());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
            onSuccess: (value) => CreatedAtAction(nameof(GetById), new { id = value }, command),
            onFailure: value => value.ToProblemDetails());
        }
    }
}