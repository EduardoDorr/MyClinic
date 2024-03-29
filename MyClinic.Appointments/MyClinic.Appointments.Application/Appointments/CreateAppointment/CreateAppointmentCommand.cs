using MediatR;

using MyClinic.Common.Results;
using MyClinic.Appointments.Domain.Entities;

namespace MyClinic.Appointments.Application.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid ProcedureId,
    DateTime StartDate,
    AppointmentType Type) : IRequest<Result<Guid>>;