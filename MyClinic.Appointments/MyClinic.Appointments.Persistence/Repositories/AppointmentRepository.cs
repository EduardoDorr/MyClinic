using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Models.Pagination;

using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Domain.Repositories;
using MyClinic.Appointments.Persistence.Contexts;

namespace MyClinic.Appointments.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly MyClinicAppointmentDbContext _dbContext;

    public AppointmentRepository(MyClinicAppointmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Appointment>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var appointments = _dbContext.Appointments.AsQueryable();

        return await appointments.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<PaginationResult<Appointment>> GetAllByPatientAsync(Guid patientId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var appointments =
            _dbContext.Appointments
            .Where(d => d.PatientId == patientId)
            .AsQueryable();

        return await appointments.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<PaginationResult<Appointment>> GetAllByDoctorAsync(Guid doctorId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var appointments =
            _dbContext.Appointments
            .Where(d => d.DoctorId == doctorId)
            .AsQueryable();

        return await appointments.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Appointments.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);
    }    

    public async Task<bool> IsUniqueAsync(Guid patientId, Guid doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var hasAppointment =
            await _dbContext.Appointments
            .Where(s => s.PatientId == patientId ||
                        s.DoctorId == doctorId)
            .AnyAsync(s => (startDate <= s.ScheduledStartDate && endDate > s.ScheduledStartDate) ||
                           (startDate < s.ScheduledEndDate && endDate >= s.ScheduledEndDate) ||
                           (startDate >= s.ScheduledStartDate && endDate <= s.ScheduledEndDate) ||
                           (startDate < s.ScheduledStartDate && endDate > s.ScheduledEndDate),
                      cancellationToken);

        return !hasAppointment;
    }

    public void Create(Appointment appointment)
    {
        _dbContext.Appointments.Add(appointment);
    }

    public void Update(Appointment appointment)
    {
        _dbContext.Appointments.Update(appointment);
    }
}