using System.Reflection;

using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Persistences.Outbox;
using MyClinic.Common.Persistences.Configurations;
using MyClinic.Doctors.Domain.Constants;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Entities.Schedules;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Persistence.Contexts;

public class MyClinicDoctorDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public MyClinicDoctorDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaConstants.DoctorSchema);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}