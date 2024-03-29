using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Appointments.Application.Appointments.UpdateAppointment;

public sealed record UpdateAppointmentCommand(
    Guid Id,
    DateTime StartDate) : IRequest<Result<Guid>>;