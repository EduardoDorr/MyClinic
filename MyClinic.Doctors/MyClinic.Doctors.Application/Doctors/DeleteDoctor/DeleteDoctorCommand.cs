using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Doctors.DeleteDoctor;

public sealed record DeleteDoctorCommand(Guid Id) : IRequest<Result>;