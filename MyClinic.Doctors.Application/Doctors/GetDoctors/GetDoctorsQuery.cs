using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Doctors.GetDoctors;

public sealed record GetDoctorsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<DoctorViewModel>>>;