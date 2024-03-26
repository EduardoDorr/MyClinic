using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.UnitOfWork;
using MyClinic.Doctors.Domain.Entities.Specialities;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Specialities.CreateSpeciality;

public sealed class CreateSpecialityCommandHandler : IRequestHandler<CreateSpecialityCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecialityRepository _specialityRepository;

    public CreateSpecialityCommandHandler(
        IUnitOfWork unitOfWork,
        ISpecialityRepository specialityRepository)
    {
        _unitOfWork = unitOfWork;
        _specialityRepository = specialityRepository;
    }

    public async Task<Result<Guid>> Handle(CreateSpecialityCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _specialityRepository.IsUniqueAsync(request.Name, cancellationToken);

        if (!isUnique)
            return Result.Fail<Guid>(SpecialityErrors.IsNotUnique);

        var specialityResult = Speciality.Create(request.Name);

        if (!specialityResult.Success)
            return Result.Fail<Guid>(specialityResult.Errors);

        var speciality = specialityResult.Value;

        _specialityRepository.Create(speciality);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(SpecialityErrors.CannotBeCreated);

        return Result.Ok(speciality.Id);
    }
}