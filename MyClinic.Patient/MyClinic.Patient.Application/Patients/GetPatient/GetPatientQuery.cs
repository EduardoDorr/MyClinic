using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Application.Patients.Models;

namespace MyClinic.Patients.Application.Patients.GetPatient;

public sealed record GetPatientQuery(Guid Id) : IRequest<Result<PatientViewModel?>>;