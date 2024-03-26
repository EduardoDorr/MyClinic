using MyClinic.Common.Persistence.UnitOfWork;

using MyClinic.Appointments.Domain.UnitOfWork;
using MyClinic.Appointments.Persistence.Contexts;

namespace MyClinic.Appointments.Persistence.UnitOfWork;

public class UnitOfWork : BaseUnitOfWork<MyClinicAppointmentDbContext>, IUnitOfWork
{
    public UnitOfWork(MyClinicAppointmentDbContext dbContext)
        : base(dbContext) { }
}