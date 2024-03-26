using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Insurances.Models;
using MyClinic.Patients.Application.Insurances.GetInsurance;
using MyClinic.Patients.Application.Insurances.GetInsurances;
using MyClinic.Patients.Application.Insurances.CreateInsurance;

namespace MyClinic.Patients.Application.Insurances.Services;

public interface IInsuranceService
{
    Task<Result<PaginationResult<InsuranceViewModel>>> GetAllAsync(GetInsurancesQuery query);
    Task<Result<InsuranceViewModel?>> GetByIdAsync(GetInsuranceQuery query);
    Task<Result<Guid>> CreateAsync(CreateInsuranceCommand command);
}