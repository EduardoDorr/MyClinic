using System.Reflection;

using Microsoft.EntityFrameworkCore;

using MyClinic.Common.Persistence.Outbox;
using MyClinic.Common.Persistence.Configurations;

using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Domain.Constants;

namespace MyClinic.Procedures.Persistence.Contexts;

public class MyClinicProcedureDbContext : DbContext
{
    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public MyClinicProcedureDbContext(DbContextOptions<MyClinicProcedureDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaConstants.ProcedureSchema);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}