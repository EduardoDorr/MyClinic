using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Specialities.Models;
using MyClinic.Doctors.Application.Specialities.GetSpeciality;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;
using MyClinic.Doctors.Application.Specialities.CreateSpeciality;
using MyClinic.Doctors.Application.Specialities.UpdateSpeciality;
using MyClinic.Doctors.Application.Specialities.DeleteSpeciality;

namespace MyClinic.Doctors.Application.Specialities.Services;

public sealed class SpecialityService : ISpecialityService
{
    private readonly ISender _sender;

    public SpecialityService(ISender mediator)
    {
        _sender = mediator;
    }

    public async Task<Result<PaginationResult<SpecialityViewModel>>> GetAllAsync(GetSpecialitiesQuery query) =>
        await _sender.Send(query);

    public async Task<Result<SpecialityDetailsViewModel?>> GetByIdAsync(GetSpecialityByIdQuery query) =>
        await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateSpecialityCommand command) =>
        await _sender.Send(command);

    public async Task<Result> UpdateAsync(Guid id, UpdateSpecialityInputModel model) =>
        await _sender.Send(model.ToCommand(id));

    public async Task<Result> DeleteAsync(DeleteSpecialityCommand command) =>
        await _sender.Send(command);
}
