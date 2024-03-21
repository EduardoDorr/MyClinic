using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Application.Doctors.GetDoctorBy;
using MyClinic.Doctors.Application.Doctors.GetDoctors;
using MyClinic.Doctors.Application.Doctors.DeleteDoctor;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;
using MyClinic.Doctors.Application.Doctors.UpdateDoctor;

namespace MyClinic.Doctors.Application.Doctors.Services;

public sealed class DoctorService : IDoctorService
{
    private readonly ISender _sender;

    public DoctorService(ISender mediator)
    {
        _sender = mediator;
    }

    //public async Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query) =>
    //    await _sender.Send(query);

    //public async Task<Result<PatientDetailsViewModel?>> GetByIdAsync(GetPatientByIdQuery query) =>
    //    await _sender.Send(query);

    //public async Task<Result<PatientDetailsViewModel?>> GetByAsync(GetPatientByQuery query) =>
    //    await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateDoctorCommand command) =>
        await _sender.Send(command);

    //public async Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model) =>
    //    await _sender.Send(model.ToCommand(id));

    //public async Task<Result> Delete(DeletePatientCommand command) =>
    //    await _sender.Send(command);
}
