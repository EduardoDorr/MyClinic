using MyClinic.Common.Results;
using MyClinic.Common.ValueObjects;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Tests.Unit.DoctorModule.Doctors;

public class DoctorTest
{
    [Fact]
    public void HasDoctorValidWithSpecialitiesAndSchedules_GetBuild_IsCreated()
    {
        // Arrange
        var specialitiesResult = new List<Result<Speciality>>
        {
            Speciality.Create("Speciality A"),
            Speciality.Create("Speciality B"),
            Speciality.Create("Speciality C"),
            Speciality.Create("Speciality D")
        };

        var specialities = specialitiesResult.Select(s => s.Value).ToList();

        var doctorSchedules = new List<DoctorSchedule>
        {
            new(DateTime.Now.AddHours(-22), DateTime.Now.AddHours(-18)),
            new(DateTime.Now.AddHours(-16), DateTime.Now.AddHours(-12)),
            new(DateTime.Now.AddHours(-10), DateTime.Now.AddHours(-6)),
            new(DateTime.Now.AddHours(-4), DateTime.Now)
        };

        var doctorBuilder = Doctor
            .CreateBuilder()
            .WithName("Fulano", "De Tal")
            .WithBirthDate(new DateTime(1990,1,1))
            .WithDocument("11111111111")
            .WithContactInfo("fulano.tal@email.com", "99999999999")
            .WithAddress("Street A", "City A", "State A", "Country A", "12345678")
            .WithMedicalInfo(BloodType.O, RhFactor.Positive, GenderType.Male)
            .WithLicenseNumber("CRM/SP123456")
            .WithSpecialities(specialities)
            .WithSchedules(doctorSchedules);

        // Act
        var doctorResult = doctorBuilder.Build();

        // Assert
        Assert.NotNull(doctorResult);
        Assert.True(doctorResult.Success);
        Assert.NotNull(doctorResult.Value);
        Assert.Equal(4, doctorResult.Value.Specialities.Count);
        Assert.Equal(4, doctorResult.Value.Schedules.Count);
    }
}