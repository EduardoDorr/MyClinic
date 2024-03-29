using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Application.Appointments.Models;
using MyClinic.Appointments.Application.Appointments.GetAppointments;
using MyClinic.Appointments.Application.Appointments.GetAppointmentById;
using MyClinic.Appointments.Application.Appointments.GetAppointmentsByDoctor;
using MyClinic.Appointments.Application.Appointments.GetAppointmentsByPatient;
using MyClinic.Appointments.Application.Appointments.CreateAppointment;
using MyClinic.Appointments.Application.Appointments.UpdateAppointment;
using MyClinic.Appointments.Application.Appointments.DeleteAppointment;

namespace MyClinic.Appointments.Application.Appointments.Services;

public sealed class AppointmentService : IAppointmentService
{
    private readonly ISender _sender;

    public AppointmentService(ISender mediator)
    {
        _sender = mediator;
    }

    public async Task<Result<PaginationResult<AppointmentViewModel>>> GetAllAsync(GetAppointmentsQuery query) =>
        await _sender.Send(query);

    public async Task<Result<PaginationResult<AppointmentViewModel>>> GetAllByPatientAsync(GetAppointmentsByPatientQuery query) =>
        await _sender.Send(query);

    public async Task<Result<PaginationResult<AppointmentViewModel>>> GetAllByDoctorAsync(GetAppointmentsByDoctorQuery query) =>
        await _sender.Send(query);

    public async Task<Result<AppointmentDetailsViewModel?>> GetByIdAsync(GetAppointmentByIdQuery query) =>
        await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateAppointmentCommand command) =>
        await _sender.Send(command);

    public async Task<Result<Guid>> UpdateAsync(Guid id, UpdateAppointmentInputModel model) =>
        await _sender.Send(model.ToCommand(id));

    public async Task<Result> DeleteAsync(DeleteAppointmentCommand command) =>
        await _sender.Send(command);
}