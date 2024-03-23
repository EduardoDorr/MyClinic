using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Specialities.Models;

namespace MyClinic.Doctors.Application.Specialities.GetSpeciality;

public sealed record GetSpecialitiesQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<SpecialityViewModel>>>;