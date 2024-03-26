using MediatR;
using MyClinic.Common.Results;

namespace MyClinic.Doctors.Application.Doctors.RemoveSchedules;

public sealed record RemoveSchedulesCommand(Guid DoctorId, List<Guid>? SchedulesId) : IRequest<Result>;