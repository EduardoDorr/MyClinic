using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyClinic.Common.Persistences.Configurations;
using MyClinic.Patients.Domain.Entities.Insurances;

namespace MyClinic.Patients.Persistence.Configurations;

internal class InsuranceConfiguration : BaseEntityConfiguration<Insurance>
{
    public override void Configure(EntityTypeBuilder<Insurance> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.BasicDiscount)
               .HasColumnType("numeric(5,2)")
               .IsRequired();

        builder.HasMany(i => i.Patients)
               .WithOne(p => p.Insurance)
               .HasForeignKey(p => p.InsuranceId);
    }
}