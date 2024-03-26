using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorsBySpeciality;

public sealed record GetDoctorsBySpecialityQuery(
    Guid SpecialityId,
    int Page = 1,
    int PageSize = 10) : IRequest<Result<PaginationResult<DoctorViewModel>>>;