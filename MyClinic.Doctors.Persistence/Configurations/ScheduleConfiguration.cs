using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyClinic.Common.Persistences.Configurations;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Persistence.Configurations;

internal class ScheduleConfiguration : BaseEntityConfiguration<Schedule>
{
    public override void Configure(EntityTypeBuilder<Schedule> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.StartDate)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(s => s.EndDate)
               .HasColumnType("datetime")
               .IsRequired();
    }
}