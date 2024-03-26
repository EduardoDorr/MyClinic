using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.UnitOfWork;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Specialities.UpdateSpeciality;

public sealed class UpdateSpecialityCommandHandler : IRequestHandler<UpdateSpecialityCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecialityRepository _specialityRepository;

    public UpdateSpecialityCommandHandler(IUnitOfWork unitOfWork, ISpecialityRepository specialityRepository)
    {
        _unitOfWork = unitOfWork;
        _specialityRepository = specialityRepository;
    }

    public async Task<Result> Handle(UpdateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var speciality = await _specialityRepository.GetByIdAsync(request.Id, cancellationToken);

        if (speciality is null)
            return Result.Fail(SpecialityErrors.NotFound);

        speciality.Update(request.Name);

        _specialityRepository.Update(speciality);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(SpecialityErrors.CannotBeUpdated);

        return Result.Ok();
    }
}