using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Patients.Application.Patients.DeletePatient;

public sealed record DeletePatientCommand(Guid Id) : IRequest<Result>;