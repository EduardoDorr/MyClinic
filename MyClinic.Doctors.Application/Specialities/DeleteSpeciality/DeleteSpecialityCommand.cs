using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Specialities.DeleteSpeciality;

public sealed record DeleteSpecialityCommand(Guid Id) : IRequest<Result>;