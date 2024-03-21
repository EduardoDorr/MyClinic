using System.Linq.Expressions;

using MediatR;

using MyClinic.Common.Results;
using MyClinic.Doctors.Domain.Interfaces;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Models;

namespace MyClinic.Doctors.Application.Doctors.GetDoctorBy;

public sealed class GetDoctorByQueryHandler : IRequestHandler<GetDoctorByQuery, Result<DoctorDetailsViewModel?>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result<DoctorDetailsViewModel?>> Handle(GetDoctorByQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByAsync(GetExpression(request), cancellationToken);

        if (doctor is null)
            return Result.Fail<DoctorDetailsViewModel?>(DoctorErrors.NotFound);

        var doctorDetailsViewModel = doctor?.ToDetailsViewModel();

        return Result.Ok(doctorDetailsViewModel);
    }

    private static Expression<Func<Doctor, bool>> GetExpression(GetDoctorByQuery request)
    {
        return p => p.Cpf.Number == request.Cpf ||
                    p.Email.Address == request.Email;
    }
}