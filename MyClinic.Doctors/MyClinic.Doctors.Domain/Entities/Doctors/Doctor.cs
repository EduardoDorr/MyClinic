using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Common.ValueObjects;
using MyClinic.Doctors.Domain.Entities.Schedules;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Common.DomainEvents;

namespace MyClinic.Doctors.Domain.Entities.Doctors;

public class Doctor : Person
{
    public string LicenseNumber { get; private set; }
    public List<Speciality> Specialities { get; private set; } = [];
    public List<Schedule> Schedules { get; private set; } = [];

    protected Doctor() { }

    protected Doctor(
        string firstName,
        string lastName,
        DateTime birthDate,
        Cpf cpf,
        Email email,
        Telephone telephone,
        Address address,
        BloodData bloodData,
        Gender gender,
        string licenseNumber,
        List<Speciality>? specialities,
        List<Schedule>? schedules)
        : base(firstName, lastName, birthDate, cpf, email, telephone, address, bloodData, gender)
    {
        LicenseNumber = licenseNumber;
        AddSpecialities(specialities);
        AddSchedules(schedules);
    }

    public static DoctorBuilder CreateBuilder() => DoctorBuilder.Create();

    public static Result<Doctor> Create(
        string firstName,
        string lastName,
        DateTime birthDate,
        Cpf cpf,
        Email email,
        Telephone telephone,
        Address address,
        BloodData bloodData,
        Gender gender,
        string licenseNumber,
        List<Speciality>? specialities,
        List<Schedule>? schedules)
    {
        var doctor =
            new Doctor(firstName,
                       lastName,
                       birthDate,
                       cpf,
                       email,
                       telephone,
                       address,
                       bloodData,
                       gender,
                       licenseNumber,
                       specialities,
                       schedules);

        return Result<Doctor>.Ok(doctor);
    }

    public void Update(Doctor doctor)
    {
        FirstName = doctor.FirstName;
        LastName = doctor.LastName;
        BirthDate = doctor.BirthDate;
        Cpf = doctor.Cpf;
        Email = doctor.Email;
        Telephone = doctor.Telephone;
        Address = doctor.Address;
        BloodData = doctor.BloodData;
        Gender = doctor.Gender;
        LicenseNumber = doctor.LicenseNumber;
    }

    public void AddSpecialities(IList<Speciality>? specialities)
    {
        if (specialities is null)
            return;

        Specialities.AddRange(specialities);
    }

    public void RemoveSpecialities(IList<Speciality>? specialities)
    {
        if (specialities is null)
            return;

        foreach (var speciality in specialities)
            Specialities.Single(s => s == speciality).Deactivate();
    }

    public void AddSchedules(IList<Schedule>? schedules)
    {
        if (schedules is null)
            return;

        Schedules.AddRange(schedules);
    }

    public void RemoveSchedules(IList<Schedule>? schedules)
    {
        if (schedules is null)
            return;

        foreach (var schedule in schedules)
            Schedules.Single(s => s == schedule).Deactivate();
    }

    public void RaiseEvent(IDomainEvent domainEvent) =>
        RaiseDomainEvent(domainEvent);
}