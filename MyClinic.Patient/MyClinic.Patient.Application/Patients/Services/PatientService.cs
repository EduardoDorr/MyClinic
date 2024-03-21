using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.GetPatients;
using MyClinic.Patients.Application.Patients.GetPatientBy;
using MyClinic.Patients.Application.Patients.GetPatientById;
using MyClinic.Patients.Application.Patients.CreatePatient;
using MyClinic.Patients.Application.Patients.UpdatePatient;
using MyClinic.Patients.Application.Patients.DeletePatient;

namespace MyClinic.Patients.Application.Patients.Services;

public sealed class PatientService : IPatientService
{
    private readonly ISender _sender;

    public PatientService(ISender mediator)
    {
        _sender = mediator;
    }

    public async Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query) =>
        await _sender.Send(query);

    public async Task<Result<PatientDetailsViewModel?>> GetByIdAsync(GetPatientByIdQuery query) =>
        await _sender.Send(query);

    public async Task<Result<PatientDetailsViewModel?>> GetByAsync(GetPatientByQuery query) =>
        await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreatePatientCommand command) =>
        await _sender.Send(command);

    public async Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model) =>
        await _sender.Send(model.ToCommand(id));

    public async Task<Result> Delete(DeletePatientCommand command) =>
        await _sender.Send(command);
}
