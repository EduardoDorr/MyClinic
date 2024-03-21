using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyClinic.Common.Persistences.Configurations;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Persistence.Configurations;

internal class DoctorConfiguration : PersonConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        base.Configure(builder);

        builder.Property(d => d.LicenseNumber)
               .HasMaxLength(12)
               .IsRequired();

        builder.HasMany(d => d.Schedules)
               .WithOne(s => s.Doctor)
               .HasForeignKey(s => s.DoctorId);

        builder.HasMany(d => d.Specialities)
               .WithMany(s => s.Doctors);
    }
}