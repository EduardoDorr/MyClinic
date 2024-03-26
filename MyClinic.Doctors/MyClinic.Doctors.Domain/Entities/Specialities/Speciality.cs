using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Domain.Entities.Specialities;

public class Speciality : BaseEntity
{
    public string Name { get; private set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = [];

    protected Speciality() { }

    private Speciality(string name)
    {
        Name = name;
    }

    public static Result<Speciality> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail<Speciality>(SpecialityErrors.NameIsRequired);

        var insurance = new Speciality(name);

        return Result<Speciality>.Ok(insurance);
    }

    public void Update(string name)
    {
        Name = name;
    }
}