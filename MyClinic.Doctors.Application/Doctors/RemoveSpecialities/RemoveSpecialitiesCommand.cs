using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Doctors.RemoveSpecialities;

public sealed record RemoveSpecialitiesCommand(Guid DoctorId, List<Guid> SpecialitiesId) : IRequest<Result>;