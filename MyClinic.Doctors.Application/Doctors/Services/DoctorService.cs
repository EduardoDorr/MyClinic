using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Application.Doctors.GetDoctors;
using MyClinic.Doctors.Application.Doctors.GetDoctorBy;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;
using MyClinic.Doctors.Application.Doctors.UpdateDoctor;
using MyClinic.Doctors.Application.Doctors.DeleteDoctor;
using MyClinic.Doctors.Application.Doctors.AddSchedules;
using MyClinic.Doctors.Application.Doctors.RemoveSchedules;
using MyClinic.Doctors.Application.Doctors.AddSpecialities;
using MyClinic.Doctors.Application.Doctors.RemoveSpecialities;

namespace MyClinic.Doctors.Application.Doctors.Services;

public sealed class DoctorService : IDoctorService
{
    private readonly ISender _sender;

    public DoctorService(ISender mediator)
    {
        _sender = mediator;
    }

    public async Task<Result<PaginationResult<DoctorViewModel>>> GetAllAsync(GetDoctorsQuery query) =>
        await _sender.Send(query);

    public async Task<Result<DoctorDetailsViewModel?>> GetByIdAsync(GetDoctorByIdQuery query) =>
        await _sender.Send(query);

    public async Task<Result<DoctorDetailsViewModel?>> GetByAsync(GetDoctorByQuery query) =>
        await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateDoctorCommand command) =>
        await _sender.Send(command);

    public async Task<Result> UpdateAsync(Guid id, UpdateDoctorInputModel model) =>
        await _sender.Send(model.ToCommand(id));

    public async Task<Result> DeleteAsync(DeleteDoctorCommand command) =>
        await _sender.Send(command);

    public async Task<Result> AddSpecialitiesAsync(Guid id, List<Guid> specialitiesId) =>
        await _sender.Send(new AddSpecialitiesCommand(id, specialitiesId));

    public async Task<Result> RemoveSpecialitiesAsync(Guid id, List<Guid> specialitiesId) =>
        await _sender.Send(new RemoveSpecialitiesCommand(id, specialitiesId));

    public async Task<Result> AddSchedulesAsync(Guid id, List<DoctorSchedule> doctorSchedules) =>
        await _sender.Send(new AddSchedulesCommand(id, doctorSchedules));

    public async Task<Result> RemoveSchedulesAsync(Guid id, List<Guid> schedulesId) =>
        await _sender.Send(new RemoveSchedulesCommand(id, schedulesId));
}
