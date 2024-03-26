using MediatR;

using MyClinic.Common.Results;

using MyClinic.Doctors.Application.Specialities.Services;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;

using MyClinic.Procedures.Domain.Entities;
using MyClinic.Procedures.Application.Procedures.Models;
using MyClinic.Procedures.Domain.Repositories;

namespace MyClinic.Procedures.Application.Procedures.GetProcedureById;

public sealed class GetProcedureByIdQueryHandler : IRequestHandler<GetProcedureByIdQuery, Result<ProcedureDetailsViewModel?>>
{
    private readonly IProcedureRepository _procedureRepository;
    private readonly ISpecialityService _specialityService;

    public GetProcedureByIdQueryHandler(IProcedureRepository procedureRepository, ISpecialityService specialityService)
    {
        _procedureRepository = procedureRepository;
        _specialityService = specialityService;
    }

    public async Task<Result<ProcedureDetailsViewModel?>> Handle(GetProcedureByIdQuery request, CancellationToken cancellationToken)
    {
        var procedure = await _procedureRepository.GetByIdAsync(request.Id, cancellationToken);

        if (procedure is null)
            return Result.Fail<ProcedureDetailsViewModel?>(ProcedureErrors.NotFound);

        var specialityResult = await _specialityService.GetByIdAsync(new GetSpecialityByIdQuery(procedure.SpecialityId));

        if (!specialityResult.Success)
            return Result.Fail<ProcedureDetailsViewModel?>(specialityResult.Errors);

        var specialityViewModel = new ProcedureSpecialityViewModel(specialityResult.Value.Id, specialityResult.Value.Name);

        var procedureViewModel = procedure?.ToDetailsViewModel(specialityViewModel);

        return Result.Ok(procedureViewModel);
    }
}