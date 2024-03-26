using MyClinic.Common.Results;
using MyClinic.Common.Builders;

namespace MyClinic.Patients.Domain.Entities.Patients;

public class PatientBuilder : PersonBuilder<Patient, PatientBuilder>
{
    private int _height;
    private decimal _weight;
    private Guid? _insuranceId;

    public static PatientBuilder Create() => new();

    public PatientBuilder WithHeightAndWeight(int height, decimal weight)
    {
        _height = height;
        _weight = weight;

        return this;
    }

    public PatientBuilder WithInsuranceId(Guid? insuranceId)
    {
        _insuranceId = insuranceId;

        return this;
    }

    public override Result<Patient> Build()
    {
        if (_errors.Count > 0)
            return Result.Fail<Patient>(_errors);

        return
            Patient.Create(
                _firstName,
                _lastName,
                _birthDate,
                _cpf,
                _email,
                _telephone,
                _address,
                _bloodData,
                _gender,
                _height,
                _weight,
                _insuranceId);
    }
}