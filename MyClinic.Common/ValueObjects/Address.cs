using MyClinic.Common.Results;
using MyClinic.Common.Results.Errors;

namespace MyClinic.Common.ValueObjects;

public sealed record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    private Address() { }

    private Address(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Result<Address> Create(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrEmpty(street) || street.Length < 5)
            return Result.Fail<Address>(AddressErrors.StreetIsTooShort);

        if (string.IsNullOrEmpty(city) || city.Length < 5)
            return Result.Fail<Address>(AddressErrors.CityIsTooShort);

        if (string.IsNullOrEmpty(state) || state.Length < 5)
            return Result.Fail<Address>(AddressErrors.StateIsTooShort);

        if (string.IsNullOrEmpty(country) || country.Length < 5)
            return Result.Fail<Address>(AddressErrors.CountryIsTooShort);

        if (string.IsNullOrEmpty(zipCode) || zipCode.Length != 8 || !int.TryParse(zipCode, out int zipCodeAsNumber))
            return Result.Fail<Address>(AddressErrors.ZipCodeIsInvalid);

        var streetTrimmed = street.Trim();
        var cityTrimmed = city.Trim();
        var stateTrimmed = state.Trim();
        var countryTrimmed = country.Trim();
        var zipCodeTrimmed = zipCode.Trim();

        var address = new Address(streetTrimmed, cityTrimmed, stateTrimmed, countryTrimmed, zipCodeTrimmed);

        return Result.Ok(address);
    }
}

public sealed record AddressErrors(string Code, string Message, ErrorType Type) : IError
{
    public static readonly Error StreetIsTooShort =
        new("Address.StreetIsTooShort", "Street is too short", ErrorType.Validation);

    public static readonly Error CityIsTooShort =
        new("Address.CityIsTooShort", "City is too short", ErrorType.Validation);

    public static readonly Error StateIsTooShort =
        new("Address.StateIsTooShort", "State is too short", ErrorType.Validation);

    public static readonly Error CountryIsTooShort =
        new("Address.CountryIsTooShort", "Country is too short", ErrorType.Validation);

    public static readonly Error ZipCodeIsInvalid =
        new("Address.ZipCodeIsInvalid", "Zip code format is invalid", ErrorType.Validation);
}