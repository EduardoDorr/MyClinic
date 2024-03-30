using MyClinic.Common.DomainEvents;

using MyClinic.Appointments.Domain.Events;
using MyClinic.Appointments.Domain.UnitOfWork;
using MyClinic.Appointments.Domain.Repositories;

namespace MyClinic.Appointments.Application.Appointments.IntegrationEvents;

public sealed class AppointmentScheduledEventHandler : IDomainEventHandler<AppointmentScheduledEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentScheduledEventHandler(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
    {
        _unitOfWork = unitOfWork;
        _appointmentRepository = appointmentRepository;
    }

    public async Task Handle(AppointmentScheduledEvent notification, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(notification.AppointmentId, cancellationToken);

        if (appointment is null)
            return;

        var appointmentResult = appointment.Scheduled(notification.EventId);

        if (!appointmentResult.Success)
            return;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}