using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Patients.Domain.Entities.Patients;

namespace MyClinic.Patients.Domain.Entities.Insurances;

public class Insurance : BaseEntity
{
    public string Name { get; private set; }
    public decimal BasicDiscount { get; private set; }

    public virtual ICollection<Patient> Patients { get; private set; }

    protected Insurance() { }

    private Insurance(string name, decimal basicDiscount)
    {
        Name = name;
        BasicDiscount = basicDiscount;
    }

    public static Result<Insurance> Create(string name, decimal basicDiscount)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail<Insurance>(InsuranceErrors.NameIsRequired);

        if (basicDiscount < 0)
            return Result.Fail<Insurance>(InsuranceErrors.DiscountCannotBeNegative);

        var insurance = new Insurance(name, basicDiscount);

        return Result<Insurance>.Ok(insurance);
    }
}