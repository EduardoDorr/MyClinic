using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorBy;

public sealed record GetDoctorByQuery(string? Cpf, string? Email) : IRequest<Result<DoctorDetailsViewModel?>>;