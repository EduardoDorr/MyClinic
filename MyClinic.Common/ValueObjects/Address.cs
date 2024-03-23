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
        var streetFormated = street.Trim();

        if (string.IsNullOrEmpty(streetFormated) || streetFormated.Length < 5)
            return Result.Fail<Address>(AddressErrors.StreetIsTooShort);

        var cityFormated = city.Trim();

        if (string.IsNullOrEmpty(cityFormated) || cityFormated.Length < 5)
            return Result.Fail<Address>(AddressErrors.CityIsTooShort);

        var stateFormated = state.Trim();

        if (string.IsNullOrEmpty(stateFormated) || stateFormated.Length < 5)
            return Result.Fail<Address>(AddressErrors.StateIsTooShort);

        var countryFormated = country.Trim();

        if (string.IsNullOrEmpty(countryFormated) || countryFormated.Length < 5)
            return Result.Fail<Address>(AddressErrors.CountryIsTooShort);

        var zipCodeFormated = FormatZipCode(zipCode);

        if (string.IsNullOrEmpty(zipCodeFormated) || zipCodeFormated.Length != 8 || !int.TryParse(zipCodeFormated, out int zipCodeAsNumber))
            return Result.Fail<Address>(AddressErrors.ZipCodeIsInvalid);

        var address = new Address(streetFormated, cityFormated, stateFormated, countryFormated, zipCodeFormated);

        return Result.Ok(address);
    }

    private static string FormatZipCode(string zipCode)
    {
        return zipCode
            .Replace("-", "")
            .Trim();
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