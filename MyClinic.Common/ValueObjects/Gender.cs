using MyClinic.Common.Results;

namespace MyClinic.Common.ValueObjects;

public sealed record Gender
{
    public GenderType Type { get; }

    private Gender() { }

    private Gender(GenderType genderType)
    {
        Type = genderType;
    }

    public static Result<Gender> Create(GenderType genderType)
    {
        var gender = new Gender(genderType);
        
        return Result<Gender>.Ok(gender);
    }

    public bool IsMale() => Type == GenderType.Male;
    public bool IsFemale() => Type == GenderType.Female;
}

public enum GenderType
{
    Undefined = 0,
    Male = 1,
    Female = 2
}