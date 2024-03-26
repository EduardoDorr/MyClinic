using System.Reflection;

using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Persistence.Outbox;
using MyClinic.Common.Persistence.Configurations;

using MyClinic.Patients.Domain.Constants;
using MyClinic.Patients.Domain.Entities.Patients;
using MyClinic.Patients.Domain.Entities.Insurances;

namespace MyClinic.Patients.Persistence.Contexts;

public class MyClinicPatientDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public MyClinicPatientDbContext(DbContextOptions<MyClinicPatientDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaConstants.PatientSchema);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}