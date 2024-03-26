using MediatR;

using MyClinic.Common.Results;

using MyClinic.Doctors.Application.Specialities.Services;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;

using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Domain.UnitOfWork;
using MyClinic.Procedures.Domain.Repositories;

namespace MyClinic.Procedures.Application.Procedures.UpdateProcedure;

public sealed class UpdateProcedureCommandHandler : IRequestHandler<UpdateProcedureCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProcedureRepository _procedureRepository;
    private readonly ISpecialityService _specialityService;

    public UpdateProcedureCommandHandler(IUnitOfWork unitOfWork, IProcedureRepository procedureRepository, ISpecialityService specialityService)
    {
        _unitOfWork = unitOfWork;
        _procedureRepository = procedureRepository;
        _specialityService = specialityService;
    }

    public async Task<Result> Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await _procedureRepository.GetByIdAsync(request.Id, cancellationToken);

        if (procedure is null)
            return Result.Fail(ProcedureErrors.NotFound);

        var specialityResult = await _specialityService.GetByIdAsync(new GetSpecialityByIdQuery(procedure.SpecialityId));

        if (!specialityResult.Success)
            return Result.Fail(specialityResult.Errors);

        procedure.Update(
            request.Name,
            request.Description,
            request.Cost,
            request.Duration,
            request.MinimumSchedulingNotice,
            request.SpecialityId);

        _procedureRepository.Update(procedure);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(ProcedureErrors.CannotBeUpdated);

        return Result.Ok();
    }
}