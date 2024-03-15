using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyClinic.Common.Entities;

namespace MyClinic.Common.Persistences.Configurations;

public abstract class PersonConfiguration<TBase> : BaseEntityConfiguration<TBase> where TBase : Person
{
    public override void Configure(EntityTypeBuilder<TBase> builder)
    {
        base.Configure(builder);

        builder.Property(d => d.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(d => d.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(d => d.BirthDate)
               .HasColumnType("date")
               .IsRequired();

        builder.OwnsOne(d => d.Cpf,
            cpf =>
            {
                cpf.Property(d => d.Number)
                   .HasColumnName("Cpf")
                   .HasMaxLength(11)
                   .IsRequired();

                cpf.HasIndex(d => d.Number)
                   .IsUnique();
            });

        builder.OwnsOne(d => d.Email,
            email =>
            {
                email.Property(d => d.Address)
                     .HasColumnName("Email")
                     .HasMaxLength(100)
                     .IsRequired();

                email.HasIndex(d => d.Address)
                     .IsUnique();
            });

        builder.OwnsOne(d => d.Telephone,
            telephone =>
            {
                telephone.Property(d => d.Number)
                     .HasColumnName("Telephone")
                     .HasMaxLength(11)
                     .IsRequired();

                telephone.HasIndex(d => d.Number)
                     .IsUnique();
            });

        builder.OwnsOne(d => d.Address,
            address =>
            {
                address.Property(a => a.Street)
                       .HasColumnName("Street")
                       .HasMaxLength(100)
                       .IsRequired();

                address.Property(a => a.City)
                       .HasColumnName("City")
                       .HasMaxLength(50)
                       .IsRequired();

                address.Property(a => a.State)
                       .HasColumnName("State")
                       .HasMaxLength(50)
                       .IsRequired();

                address.Property(a => a.Country)
                       .HasColumnName("Country")
                       .HasMaxLength(50)
                       .IsRequired();

                address.Property(a => a.ZipCode)
                       .HasColumnName("ZipCode")
                       .HasMaxLength(8)
                       .IsRequired();
            });

        builder.OwnsOne(d => d.BloodData,
            bloodData =>
            {
                bloodData.Property(d => d.BloodType)
                         .HasColumnName("BloodType")
                         .IsRequired();

                bloodData.Property(d => d.RhFactor)
                         .HasColumnName("RhFactor")
                         .IsRequired();
            });

        builder.OwnsOne(d => d.Gender,
            gender =>
            {
                gender.Property(d => d.Type)
                      .HasColumnName("Gender")
                      .IsRequired();
            });
    }
}