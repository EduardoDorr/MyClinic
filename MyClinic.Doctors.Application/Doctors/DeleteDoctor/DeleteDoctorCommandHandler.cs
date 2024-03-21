using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Persistences.UnitOfWork;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;

namespace MyClinic.Doctors.Application.Doctors.DeleteDoctor;

public sealed class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork, IDoctorRepository doctorRepository)
    {
        _unitOfWork = unitOfWork;
        _doctorRepository = doctorRepository;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doctor is null)
            return Result.Fail(DoctorErrors.NotFound);

        doctor.Deactivate();

        _doctorRepository.Update(doctor);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
            return Result.Fail(DoctorErrors.CannotBeDeleted);

        return Result.Ok();
    }
}