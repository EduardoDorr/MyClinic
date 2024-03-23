using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Specialities.Models;
using MyClinic.Doctors.Application.Specialities.GetSpeciality;
using MyClinic.Doctors.Application.Specialities.GetSpecialityById;
using MyClinic.Doctors.Application.Specialities.CreateSpeciality;
using MyClinic.Doctors.Application.Specialities.UpdateSpeciality;
using MyClinic.Doctors.Application.Specialities.DeleteSpeciality;

namespace MyClinic.Doctors.Application.Specialities.Services;

public interface ISpecialityService
{
    Task<Result<PaginationResult<SpecialityViewModel>>> GetAllAsync(GetSpecialitiesQuery query);
    Task<Result<SpecialityDetailsViewModel?>> GetByIdAsync(GetSpecialityByIdQuery query);
    Task<Result<Guid>> CreateAsync(CreateSpecialityCommand command);
    Task<Result> UpdateAsync(Guid id, UpdateSpecialityInputModel model);
    Task<Result> DeleteAsync(DeleteSpecialityCommand command);
}