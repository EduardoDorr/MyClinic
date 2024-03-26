using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClinic.Common.Persistence.Configurations;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Persistence.Configurations;

internal class SpecialityConfiguration : BaseEntityConfiguration<Speciality>
{
    public override void Configure(EntityTypeBuilder<Speciality> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name)
               .HasMaxLength(50)
               .IsRequired();
    }
}