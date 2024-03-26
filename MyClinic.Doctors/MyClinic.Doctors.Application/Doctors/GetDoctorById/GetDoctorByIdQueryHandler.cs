using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Domain.Repositories;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorById;

public sealed class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Result<DoctorDetailsViewModel?>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result<DoctorDetailsViewModel?>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doctor is null)
            return Result.Fail<DoctorDetailsViewModel?>(DoctorErrors.NotFound);

        var doctorDetailsViewModel = doctor?.ToDetailsViewModel();

        return Result.Ok(doctorDetailsViewModel);
    }
}