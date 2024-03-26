using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatientBy;

public sealed record GetPatientByQuery(string? Cpf, string? Email) : IRequest<Result<PatientDetailsViewModel?>>;