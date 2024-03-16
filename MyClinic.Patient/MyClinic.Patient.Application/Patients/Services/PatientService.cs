using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.GetPatient;
using MyClinic.Patients.Application.Patients.GetPatients;
using MyClinic.Patients.Application.Patients.CreatePatient;
using MyClinic.Patients.Application.Patients.UpdatePatient;
using MyClinic.Patients.Application.Patients.DeletePatient;

namespace MyClinic.Patients.Application.Patients.Services;

public sealed class PatientService : IPatientService
{
    private readonly IMediator _mediator;

    public PatientService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query) =>
        await _mediator.Send(query);

    public async Task<Result<PatientViewModel?>> GetByIdAsync(GetPatientQuery query) =>
        await _mediator.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreatePatientCommand command) =>
        await _mediator.Send(command);

    public async Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model) =>
        await _mediator.Send(model.ToCommand(id));

    public async Task<Result> Delete(DeletePatientCommand command) =>
        await _mediator.Send(command);
}
