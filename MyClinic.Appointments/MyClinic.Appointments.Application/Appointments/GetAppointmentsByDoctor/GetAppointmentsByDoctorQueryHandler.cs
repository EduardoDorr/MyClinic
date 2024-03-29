using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Application.Appointments.Models;

namespace MyClinic.Appointments.Application.Appointments.GetAppointmentsByDoctor;

public sealed class GetAppointmentsByDoctorQueryHandler : IRequestHandler<GetAppointmentsByDoctorQuery, Result<PaginationResult<AppointmentViewModel>>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentsByDoctorQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result<PaginationResult<AppointmentViewModel>>> Handle(GetAppointmentsByDoctorQuery request, CancellationToken cancellationToken)
    {
        var paginationAppointments = await _appointmentRepository.GetAllByDoctorAsync(request.DoctorId, request.Page, request.PageSize, cancellationToken);

        var appointmentsViewModel = paginationAppointments.Data.ToViewModel();

        var paginationAppointmentsViewModel =
            new PaginationResult<AppointmentViewModel>
            (
                paginationAppointments.Page,
                paginationAppointments.PageSize,
                paginationAppointments.TotalCount,
                paginationAppointments.TotalPages,
                appointmentsViewModel.ToList()
            );

        return Result.Ok(paginationAppointmentsViewModel);
    }
}