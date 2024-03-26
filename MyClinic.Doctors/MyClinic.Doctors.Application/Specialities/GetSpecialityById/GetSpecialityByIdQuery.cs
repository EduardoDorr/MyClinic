using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Application.Specialities.Models;

namespace MyClinic.Doctors.Application.Specialities.GetSpecialityById;

public sealed record GetSpecialityByIdQuery(Guid Id) : IRequest<Result<SpecialityDetailsViewModel?>>;