using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Application.Appointments.Models;

namespace MyClinic.Appointments.Application.Appointments.GetAppointmentsByPatient;

public sealed record GetAppointmentsByPatientQuery(
    Guid PatientId,
    int Page = 1,
    int PageSize = 10) : IRequest<Result<PaginationResult<AppointmentViewModel>>>;