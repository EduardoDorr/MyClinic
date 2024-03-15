using MyClinic.Common.Results;
using MyClinic.Common.ValueObjects;

namespace MyClinic.Patients.Domain.Entities.Patients;

public class PatientBuilder
{
    private string _firstName;
    private string _lastName;
    private DateTime _birthDate;
    private string _cpf;
    private string _email;
    private string _telephone;
    private string _street;
    private string _city;
    private string _state;
    private string _country;
    private string _zipCode;
    private BloodType _bloodType;
    private RhFactor _rhFactor;
    private GenderType _gender;
    private int _height;
    private decimal _weight;
    private Guid? _insuranceId;

    protected PatientBuilder() { }

    public static PatientBuilder Create() => new();

    public PatientBuilder WithName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;

        return this;
    }

    public PatientBuilder WithBirthDate(DateTime birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public PatientBuilder WithDocument(string cpf)
    {
        _cpf = cpf;

        return this;
    }

    public PatientBuilder WithContactInfo(string email, string telephone)
    {
        _email = email;
        _telephone = telephone;

        return this;
    }

    public PatientBuilder WithAddress(string street, string city, string state, string country, string zipCode)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;
        _zipCode = zipCode;

        return this;
    }

    public PatientBuilder WithMedicalInfo(BloodType bloodType, RhFactor rhFactor, GenderType gender, int height, decimal weight)
    {
        _bloodType = bloodType;
        _rhFactor = rhFactor;
        _gender = gender;
        _height = height;
        _weight = weight;

        return this;
    }

    public PatientBuilder WithInsuranceId(Guid? insuranceId)
    {
        _insuranceId = insuranceId;

        return this;
    }

    public Result<Patient> Build()
    {
        var cpfResult = Cpf.Create(_cpf);

        if (!cpfResult.Success)
            return Result.Fail<Patient>(cpfResult.Errors[0]);

        var emailResult = Email.Create(_email);

        if (!emailResult.Success)
            return Result.Fail<Patient>(emailResult.Errors[0]);

        var telephoneResult = Telephone.Create(_telephone);

        if (!telephoneResult.Success)
            return Result.Fail<Patient>(telephoneResult.Errors[0]);

        var addressResult = Address.Create(_street, _city, _state, _country, _zipCode);

        if (!addressResult.Success)
            return Result.Fail<Patient>(addressResult.Errors[0]);

        var bloodDataResult = BloodData.Create(_bloodType, _rhFactor);

        if (!bloodDataResult.Success)
            return Result.Fail<Patient>(bloodDataResult.Errors[0]);

        var genderResult = Gender.Create(_gender);

        if (!genderResult.Success)
            return Result.Fail<Patient>(genderResult.Errors[0]);

        return
            Patient.Create(
                _firstName,
                _lastName,
                _birthDate,
                cpfResult.Value,
                emailResult.Value,
                telephoneResult.Value,
                addressResult.Value,
                bloodDataResult.Value,
                genderResult.Value,
                _height,
                _weight,
                _insuranceId);
    }
}