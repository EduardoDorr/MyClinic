using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Appointments.Application.Appointments.DeleteAppointment;

public sealed record DeleteAppointmentCommand(Guid Id) : IRequest<Result>;