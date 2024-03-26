using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClinic.Common.Persistence.Configurations;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Persistence.Configurations;

internal class PatientConfiguration : PersonConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Height)
               .IsRequired();

        builder.Property(p => p.Weight)
               .HasColumnType("numeric(6,2)")
               .IsRequired();

        builder.HasOne(p => p.Insurance)
               .WithMany(i => i.Patients)
               .HasForeignKey(p => p.InsuranceId);
    }
}