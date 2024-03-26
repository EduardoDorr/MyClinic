using MyClinic.Common.Persistence.Repositories;

using MyClinic.Appointments.Domain.Entities;
using MyClinic.Common.Models.Pagination;

namespace MyClinic.Appointments.Domain.Repositories;

public interface IAppointmentRepository
    : IReadableRepository<Appointment>,
      ICreatableRepository<Appointment>,
      IUpdatableRepository<Appointment>
{
    Task<PaginationResult<Appointment>> GetAllByDoctorAsync(Guid doctorId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<PaginationResult<Appointment>> GetAllByPatientAsync(Guid patientId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<bool> IsUniqueAsync(Guid patientId, Guid doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}