using MediatR;

using MyClinic.Common.Results;

using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Application.Appointments.Models;

namespace MyClinic.Appointments.Application.Appointments.GetAppointmentById;

public sealed class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Result<AppointmentDetailsViewModel?>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result<AppointmentDetailsViewModel?>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (appointment is null)
            return Result.Fail<AppointmentDetailsViewModel?>(AppointmentErrors.NotFound);

        var appointmentDetailsViewModel = appointment?.ToDetailsViewModel();

        return Result.Ok(appointmentDetailsViewModel);
    }
}