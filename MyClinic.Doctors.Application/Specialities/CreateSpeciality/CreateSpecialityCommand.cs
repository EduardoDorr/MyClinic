using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Specialities.CreateSpeciality;

public sealed record CreateSpecialityCommand(string Name) : IRequest<Result<Guid>>;