using MyClinic.Common.Entities;
using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;
using MyClinic.Common.ValueObjects;

namespace MyClinic.Common.Builders;

public abstract class PersonBuilder<TPerson, TBuilder>
    where TPerson : Person
    where TBuilder : PersonBuilder<TPerson, TBuilder>
{
    private readonly TBuilder _builderInstance = null;

    protected string _firstName;
    protected string _lastName;
    protected DateTime _birthDate;
    protected Cpf _cpf;
    protected Email _email;
    protected Telephone _telephone;
    protected Address _address;
    protected BloodData _bloodData;
    protected Gender _gender;

    protected List<IError> _errors = [];

    protected PersonBuilder()
    {
        _builderInstance = (TBuilder)this;
    }

    public TBuilder WithName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;

        return _builderInstance;
    }

    public TBuilder WithBirthDate(DateTime birthDate)
    {
        _birthDate = birthDate;

        return _builderInstance;
    }

    public TBuilder WithDocument(string cpf)
    {
        var cpfResult = Cpf.Create(cpf);

        if (!cpfResult.Success)
            _errors.AddRange(cpfResult.Errors);

        _cpf = cpfResult.ValueOrDefault;

        return _builderInstance;
    }

    public TBuilder WithContactInfo(string email, string telephone)
    {
        var emailResult = Email.Create(email);

        if (!emailResult.Success)
            _errors.AddRange(emailResult.Errors);

        var telephoneResult = Telephone.Create(telephone);

        if (!telephoneResult.Success)
            _errors.AddRange(telephoneResult.Errors);

        _email = emailResult.ValueOrDefault;
        _telephone = telephoneResult.ValueOrDefault;

        return _builderInstance;
    }

    public TBuilder WithAddress(string street, string city, string state, string country, string zipCode)
    {
        var addressResult = Address.Create(street, city, state, country, zipCode);

        if (!addressResult.Success)
            _errors.AddRange(addressResult.Errors);

        _address = addressResult.ValueOrDefault;

        return _builderInstance;
    }

    public TBuilder WithMedicalInfo(BloodType bloodType, RhFactor rhFactor, GenderType gender)
    {
        var bloodDataResult = BloodData.Create(bloodType, rhFactor);

        if (!bloodDataResult.Success)
            _errors.AddRange(bloodDataResult.Errors);

        var genderResult = Gender.Create(gender);

        if (!genderResult.Success)
            _errors.AddRange(genderResult.Errors);

        _bloodData = bloodDataResult.ValueOrDefault;
        _gender = genderResult.ValueOrDefault;

        return _builderInstance;
    }    

    public abstract Result<TPerson> Build();
}