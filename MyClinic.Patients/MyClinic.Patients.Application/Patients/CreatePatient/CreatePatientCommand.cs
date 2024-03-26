using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.ValueObjects;
using MyClinic.Common.Models.InputModels;

namespace MyClinic.Patients.Application.Patients.CreatePatient;

public record CreatePatientCommand(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Cpf,
    string Email,
    string Telephone,
    AddressInputModel Address,
    BloodDataInputModel BloodData,
    GenderType Gender,
    int Height,
    decimal Weight,
    Guid? InsuranceId) : IRequest<Result<Guid>>;