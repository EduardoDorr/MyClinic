using MediatR;

using MyClinic.Common.Results;
using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Domain.Repositories;
using MyClinic.Procedures.Domain.UnitOfWork;

namespace MyClinic.Procedures.Application.Procedures.DeleteProcedure;

public sealed class DeleteProcedureCommandHandler : IRequestHandler<DeleteProcedureCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProcedureRepository _procedureRepository;

    public DeleteProcedureCommandHandler(IUnitOfWork unitOfWork, IProcedureRepository procedureRepository)
    {
        _unitOfWork = unitOfWork;
        _procedureRepository = procedureRepository;
    }

    public async Task<Result> Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await _procedureRepository.GetByIdAsync(request.Id, cancellationToken);

        if (procedure is null)
            return Result.Fail(ProcedureErrors.NotFound);

        procedure.Deactivate();

        _procedureRepository.Update(procedure);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(ProcedureErrors.CannotBeDeleted);

        return Result.Ok();
    }
}