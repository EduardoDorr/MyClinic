using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Specialities;

namespace MyClinic.Doctors.Application.Specialities.DeleteSpeciality;

public sealed class DeleteSpecialityCommandHandler : IRequestHandler<DeleteSpecialityCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecialityRepository _specialityRepository;

    public DeleteSpecialityCommandHandler(IUnitOfWork unitOfWork, ISpecialityRepository specialityRepository)
    {
        _unitOfWork = unitOfWork;
        _specialityRepository = specialityRepository;
    }

    public async Task<Result> Handle(DeleteSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _specialityRepository.GetByIdAsync(request.Id, cancellationToken);

        if (speciality is null)
            return Result.Fail(SpecialityErrors.NotFound);

        speciality.Deactivate();

        _specialityRepository.Update(speciality);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(SpecialityErrors.CannotBeDeleted);

        return Result.Ok();
    }
}