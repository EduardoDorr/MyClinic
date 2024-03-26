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

public interface IProcedureService
{
    Task<Result<PaginationResult<ProcedureViewModel>>> GetAllAsync(GetProceduresQuery query);
    Task<Result<PaginationResult<ProcedureViewModel>>> GetAllBySpecialityAsync(GetProceduresBySpecialityQuery query);
    Task<Result<ProcedureDetailsViewModel?>> GetByIdAsync(GetProcedureByIdQuery query);
    Task<Result<Guid>> CreateAsync(CreateProcedureCommand command);
    Task<Result> UpdateAsync(Guid id, UpdateProcedureInputModel model);
    Task<Result> DeleteAsync(DeleteProcedureCommand command);
}