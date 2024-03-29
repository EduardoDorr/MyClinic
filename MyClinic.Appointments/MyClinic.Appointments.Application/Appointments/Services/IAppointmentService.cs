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

public interface IAppointmentService
{
    Task<Result<PaginationResult<AppointmentViewModel>>> GetAllAsync(GetAppointmentsQuery query);
    Task<Result<PaginationResult<AppointmentViewModel>>> GetAllByPatientAsync(GetAppointmentsByPatientQuery query);
    Task<Result<PaginationResult<AppointmentViewModel>>> GetAllByDoctorAsync(GetAppointmentsByDoctorQuery query);
    Task<Result<AppointmentDetailsViewModel?>> GetByIdAsync(GetAppointmentByIdQuery query);
    Task<Result<Guid>> CreateAsync(CreateAppointmentCommand command);
    Task<Result<Guid>> UpdateAsync(Guid id, UpdateAppointmentInputModel model);
    Task<Result> DeleteAsync(DeleteAppointmentCommand command);
}