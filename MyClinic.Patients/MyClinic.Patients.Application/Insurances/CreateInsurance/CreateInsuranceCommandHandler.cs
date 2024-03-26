using MediatR;

using MyClinic.Common.Results;
using MyClinic.Patients.Domain.UnitOfWork;
using MyClinic.Patients.Domain.Entities.Insurances;
using MyClinic.Patients.Domain.Repositories;

namespace MyClinic.Patients.Application.Insurances.CreateInsurance;

public sealed class CreateInsuranceCommandHandler : IRequestHandler<CreateInsuranceCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInsuranceRepository _insuranceRepository;

    public CreateInsuranceCommandHandler(IUnitOfWork unitOfWork, IInsuranceRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _insuranceRepository = patientRepository;
    }

    public async Task<Result<Guid>> Handle(CreateInsuranceCommand request, CancellationToken cancellationToken)
    {
        var insuranceResult = Insurance.Create(request.Name, request.BasicDiscount);

        if (!insuranceResult.Success)
            return Result.Fail<Guid>(insuranceResult.Errors);

        var insurance = insuranceResult.Value;

        _insuranceRepository.Create(insurance);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(InsuranceErrors.CannotBeCreated);

        return Result.Ok(insurance.Id);
    }
}