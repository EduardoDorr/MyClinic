using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorById;

public sealed record GetDoctorByIdQuery(Guid Id) : IRequest<Result<DoctorDetailsViewModel?>>;