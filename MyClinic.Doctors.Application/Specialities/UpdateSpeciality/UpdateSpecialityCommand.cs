using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Specialities.UpdateSpeciality;

public sealed record UpdateSpecialityCommand(
    Guid Id,
    string Name) : IRequest<Result>;