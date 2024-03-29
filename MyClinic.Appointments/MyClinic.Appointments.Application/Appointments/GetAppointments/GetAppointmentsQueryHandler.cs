using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Application.Appointments.Models;

namespace MyClinic.Appointments.Application.Appointments.GetAppointments;

public sealed class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, Result<PaginationResult<AppointmentViewModel>>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result<PaginationResult<AppointmentViewModel>>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var paginationAppointments = await _appointmentRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var AppointmentsViewModel = paginationAppointments.Data.ToViewModel();

        var paginationAppointmentsViewModel =
            new PaginationResult<AppointmentViewModel>
            (
                paginationAppointments.Page,
                paginationAppointments.PageSize,
                paginationAppointments.TotalCount,
                paginationAppointments.TotalPages,
                AppointmentsViewModel.ToList()
            );

        return Result.Ok(paginationAppointmentsViewModel);
    }
}