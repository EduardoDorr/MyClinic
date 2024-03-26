using MyClinic.Common.Results;
using MyClinic.Common.Builders;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Domain.Entities.Schedules;

namespace MyClinic.Doctors.Domain.Entities.Doctors;

public class DoctorBuilder : PersonBuilder<Doctor, DoctorBuilder>
{
    private string _licenseNumber;
    private readonly List<Speciality> _specialities = [];
    private readonly List<DoctorSchedule> _schedules = [];

    public static DoctorBuilder Create() => new();

    public DoctorBuilder WithLicenseNumber(string licenseNumber)
    {
        _licenseNumber = licenseNumber;

        return this;
    }

    public DoctorBuilder WithSpecialities(List<Speciality>? specialities)
    {
        if (specialities is null)
            return this;

        _specialities.AddRange(specialities);

        return this;
    }

    public DoctorBuilder WithSchedules(List<DoctorSchedule>? doctorSchedules)
    {
        if (doctorSchedules is null)
            return this;

        _schedules.AddRange(doctorSchedules);

        return this;
    }

    public override Result<Doctor> Build()
    {
        if (_errors.Count > 0)
            return Result.Fail<Doctor>(_errors);

        var doctorResult =
            Doctor.Create(
                _firstName,
                _lastName,
                _birthDate,
                _cpf,
                _email,
                _telephone,
                _address,
                _bloodData,
                _gender,
                _licenseNumber,
                _specialities,
                new());

        if (!doctorResult.Success)
            return Result.Fail<Doctor>(doctorResult.Errors);

        var doctorId = doctorResult.Value.Id;

        var schedulesResult = _schedules.Select(s => Schedule.Create(s.StartDate, s.EndDate, doctorId));

        if (schedulesResult.Any(s => !s.Success))
            return Result.Fail<Doctor>(schedulesResult.SelectMany(s => s.Errors));

        var schedules = schedulesResult.Select(s => s.Value);

        doctorResult.Value.AddSchedules(schedules.ToList());

        return doctorResult;
    }
}