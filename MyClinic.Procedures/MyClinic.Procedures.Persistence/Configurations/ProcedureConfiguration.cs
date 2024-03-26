using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClinic.Common.Persistence.Configurations;
using MyClinic.Procedures.Domain.Entities;

namespace MyClinic.Procedures.Persistence.Configurations;

internal class ProcedureConfiguration : BaseEntityConfiguration<Procedure>
{
    public override void Configure(EntityTypeBuilder<Procedure> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(b => b.Name);

        builder.Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Cost)
            .HasColumnType("numeric(8,2)")
            .IsRequired();

        builder.Property(p => p.Duration)
            .IsRequired();

        builder.Property(p => p.MinimumSchedulingNotice)
            .IsRequired();

        builder.Property(b => b.SpecialityId)
            .IsRequired();

        builder.HasIndex(b => b.SpecialityId);
    }
}