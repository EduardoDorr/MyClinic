using MediatR;

using MyClinic.Common.Results;

using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Domain.UnitOfWork;
using MyClinic.Appointments.Domain.Repositories;

namespace MyClinic.Appointments.Application.Appointments.DeleteAppointment;

public sealed class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
    {
        _unitOfWork = unitOfWork;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (appointment is null)
            return Result.Fail(AppointmentErrors.NotFound);

        appointment.Deactivate();

        _appointmentRepository.Update(appointment);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(AppointmentErrors.CannotBeDeleted);

        return Result.Ok();
    }
}