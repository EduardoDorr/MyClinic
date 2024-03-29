using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Application.Appointments.Models;

namespace MyClinic.Appointments.Application.Appointments.GetAppointmentsByPatient;

public sealed class GetAppointmentsByPatientQueryHandler : IRequestHandler<GetAppointmentsByPatientQuery, Result<PaginationResult<AppointmentViewModel>>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentsByPatientQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Result<PaginationResult<AppointmentViewModel>>> Handle(GetAppointmentsByPatientQuery request, CancellationToken cancellationToken)
    {
        var paginationAppointments = await _appointmentRepository.GetAllByPatientAsync(request.PatientId, request.Page, request.PageSize, cancellationToken);

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