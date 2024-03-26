using MediatR;

using MyClinic.Common.Results;

using MyClinic.Doctors.Application.Specialities.Services;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;

using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Domain.UnitOfWork;
using MyClinic.Procedures.Domain.Repositories;

namespace MyClinic.Procedures.Application.Procedures.CreateProcedure;

public sealed class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProcedureRepository _procedureRepository;
    private readonly ISpecialityService _specialityService;

    public CreateProcedureCommandHandler(
        IUnitOfWork unitOfWork,
        IProcedureRepository procedureRepository,
        ISpecialityService specialityService)
    {
        _unitOfWork = unitOfWork;
        _procedureRepository = procedureRepository;
        _specialityService = specialityService;
    }

    public async Task<Result<Guid>> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _procedureRepository.IsUniqueAsync(request.Name, request.SpecialityId, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(ProcedureErrors.IsNotUnique);

        var specialityResult = await _specialityService.GetByIdAsync(new GetSpecialityByIdQuery(request.SpecialityId));

        if (!specialityResult.Success)
            return Result.Fail<Guid>(specialityResult.Errors);

        var procedureResult =
            Procedure.Create(
                request.Name,
                request.Description,
                request.Cost,
                request.Duration,
                request.MinimumSchedulingNotice,
                request.SpecialityId);

        if (!procedureResult.Success)
            return Result.Fail<Guid>(procedureResult.Errors);

        var procedure = procedureResult.Value;

        _procedureRepository.Create(procedure);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(ProcedureErrors.CannotBeCreated);

        return Result.Ok(procedure.Id);
    }
}