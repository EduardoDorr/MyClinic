using MyClinic.Common.Results;

namespace MyClinic.Common.ValueObjects;

public sealed record BloodData
{
    public BloodType BloodType { get; }
    public RhFactor RhFactor { get; }

    private BloodData() { }

    private BloodData(BloodType bloodType, RhFactor rhFactor)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
    }

    public static Result<BloodData> Create(BloodType bloodType, RhFactor rhFactor)
    {
        var bloodData = new BloodData(bloodType, rhFactor);

        return Result<BloodData>.Ok(bloodData);
    }
}

public enum BloodType
{
    O,
    A,
    B,
    AB
}

public enum RhFactor
{
    Positive,
    Negative
}