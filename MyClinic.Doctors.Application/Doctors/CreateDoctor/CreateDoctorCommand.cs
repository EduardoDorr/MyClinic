using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.ValueObjects;
using MyClinic.Common.Models.InputModels;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Application.Doctors.CreateDoctor;

public sealed record CreateDoctorCommand(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string Cpf,
    string Email,
    string Telephone,
    AddressInputModel Address,
    BloodDataInputModel BloodData,
    GenderType Gender,
    string LicenseNumber,
    List<Guid>? SpecialitiesId,
    List<DoctorSchedule>? Schedules) : IRequest<Result<Guid>>;