using MediatR;

using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Procedures.Application.Procedures.Models;
using MyClinic.Procedures.Application.Procedures.GetProcedures;
using MyClinic.Procedures.Application.Procedures.GetProcedureById;
using MyClinic.Procedures.Application.Procedures.GetProceduresBySpeciality;
using MyClinic.Procedures.Application.Procedures.CreateProcedure;
using MyClinic.Procedures.Application.Procedures.UpdateProcedure;
using MyClinic.Procedures.Application.Procedures.DeleteProcedure;

namespace MyClinic.Procedures.Application.Procedures.Services;

public sealed class ProcedureService : IProcedureService
{
    private readonly ISender _sender;

    public ProcedureService(ISender mediator)
    {
        _sender = mediator;
    }

    public async Task<Result<PaginationResult<ProcedureViewModel>>> GetAllAsync(GetProceduresQuery query) =>
        await _sender.Send(query);

    public async Task<Result<PaginationResult<ProcedureViewModel>>> GetAllBySpecialityAsync(GetProceduresBySpecialityQuery query) =>
        await _sender.Send(query);

    public async Task<Result<ProcedureDetailsViewModel?>> GetByIdAsync(GetProcedureByIdQuery query) =>
        await _sender.Send(query);

    public async Task<Result<Guid>> CreateAsync(CreateProcedureCommand command) =>
        await _sender.Send(command);

    public async Task<Result> UpdateAsync(Guid id, UpdateProcedureInputModel model) =>
        await _sender.Send(model.ToCommand(id));

    public async Task<Result> DeleteAsync(DeleteProcedureCommand command) =>
        await _sender.Send(command);
}