using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Common.ValueObjects;
using MyClinic.Patients.Domain.Entities.Insurances;
using MyClinic.Common.DomainEvents;

namespace MyClinic.Patients.Domain.Entities.Patients;

public class Patient : Person
{
    public int Height { get; private set; }
    public decimal Weight { get; private set; }
    public Guid? InsuranceId { get; private set; }

    public virtual Insurance Insurance { get; private set; }

    protected Patient() { }

    protected Patient(
        string firstName,
        string lastName,
        DateTime birthDate,
        Cpf cpf,
        Email email,
        Telephone telephone,
        Address address,
        BloodData bloodData,
        Gender gender,
        int height,
        decimal weight,
        Guid? insuranceId)
        : base(firstName, lastName, birthDate, cpf, email, telephone, address, bloodData, gender)
    {
        Height = height;
        Weight = weight;
        InsuranceId = insuranceId;
    }

    public static PatientBuilder CreateBuilder() => PatientBuilder.Create();

    public static Result<Patient> Create(
        string firstName,
        string lastName,
        DateTime birthDate,
        Cpf cpf,
        Email email,
        Telephone telephone,
        Address address,
        BloodData bloodData,
        Gender gender,
        int height,
        decimal weight,
        Guid? insuranceId)
    {
        var patient =
            new Patient(firstName,
                        lastName,
                        birthDate,
                        cpf,
                        email,
                        telephone,
                        address,
                        bloodData,
                        gender,
                        height,
                        weight,
                        insuranceId);

        return Result<Patient>.Ok(patient);
    }

    public void Update(Patient patient)
    {
        FirstName = patient.FirstName;
        LastName = patient.LastName;
        BirthDate = patient.BirthDate;
        Cpf = patient.Cpf;
        Email = patient.Email;
        Telephone = patient.Telephone;
        Address = patient.Address;
        BloodData = patient.BloodData;
        Gender = patient.Gender;
        Height = patient.Height;
        Weight = patient.Weight;
        InsuranceId = patient.InsuranceId;
    }

    public void RaiseEvent(IDomainEvent domainEvent) =>
        RaiseDomainEvent(domainEvent);
}