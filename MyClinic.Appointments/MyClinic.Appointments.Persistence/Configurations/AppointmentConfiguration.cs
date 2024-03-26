using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyClinic.Common.Persistence.Configurations;

using MyClinic.Appointments.Domain.Entities;

namespace MyClinic.Appointments.Persistence.Configurations;

internal class AppointmentConfiguration : BaseEntityConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        base.Configure(builder);

        builder.Property(d => d.PatientId)
            .IsRequired();

        builder.Property(d => d.DoctorId)
            .IsRequired();

        builder.Property(d => d.ProcedureId)
            .IsRequired();

        builder.Property(d => d.ScheduledStartDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(d => d.ScheduledEndDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(d => d.RealStartDate)
            .HasColumnType("datetime");

        builder.Property(d => d.RealEndDate)
            .HasColumnType("datetime");

        builder.Property(d => d.CancellationDate)
            .HasColumnType("datetime");

        builder.Property(d => d.Type)
            .IsRequired();

        builder.Property(d => d.Status)
            .IsRequired();
    }
}