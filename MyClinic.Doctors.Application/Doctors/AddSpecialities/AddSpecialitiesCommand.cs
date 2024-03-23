using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Doctors.AddSpecialities;

public sealed record AddSpecialitiesCommand(Guid DoctorId, List<Guid> SpecialitiesId) : IRequest<Result>;