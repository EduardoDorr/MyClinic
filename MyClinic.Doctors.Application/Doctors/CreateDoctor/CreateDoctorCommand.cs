using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.ValueObjects;
using MyClinic.Common.Models.InputModels;

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
    List<ScheduleInputModel>? Schedules) : IRequest<Result<Guid>>;

public sealed record ScheduleInputModel(DateTime StartDate, DateTime EndDate);