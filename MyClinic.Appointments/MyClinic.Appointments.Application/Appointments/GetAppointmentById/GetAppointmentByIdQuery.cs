using MediatR;
using MyClinic.Appointments.Application.Appointments.Models;
using MyClinic.Common.Results;

namespace MyClinic.Appointments.Application.Appointments.GetAppointmentById;

public sealed record GetAppointmentByIdQuery(Guid Id) : IRequest<Result<AppointmentDetailsViewModel?>>;