using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatients;

public sealed record GetPatientsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<PatientViewModel>>>;