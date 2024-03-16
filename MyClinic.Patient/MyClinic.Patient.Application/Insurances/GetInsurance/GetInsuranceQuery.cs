using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Application.Insurances.Models;

namespace MyClinic.Patients.Application.Insurances.GetInsurance;

public sealed record GetInsuranceQuery(Guid Id) : IRequest<Result<InsuranceViewModel?>>;