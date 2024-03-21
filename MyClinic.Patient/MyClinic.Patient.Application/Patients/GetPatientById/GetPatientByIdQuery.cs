using MediatR;
using MyClinic.Common.Results;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatientById;

public sealed record GetPatientByIdQuery(Guid Id) : IRequest<Result<PatientDetailsViewModel?>>;