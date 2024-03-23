using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Application.Doctors.AddSchedules;

public sealed record AddSchedulesCommand(Guid DoctorId, List<DoctorSchedule> Schedules) : IRequest<Result>;