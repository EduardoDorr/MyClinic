using MediatR;

using MyClinic.Common.Results;

namespace MyClinic.Patients.Application.Insurances.CreateInsurance;

public sealed record CreateInsuranceCommand(string Name, decimal BasicDiscount) : IRequest<Result<Guid>>;