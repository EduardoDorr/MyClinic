using System.Reflection;

using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Persistence.Outbox;
using MyClinic.Common.Persistence.Configurations;

using MyClinic.Appointments.Domain.Entities;
using MyClinic.Appointments.Domain.Constants;

namespace MyClinic.Appointments.Persistence.Contexts;

public class MyClinicAppointmentDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public MyClinicAppointmentDbContext(DbContextOptions<MyClinicAppointmentDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaConstants.AppointmentSchema);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}