using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Domain.Entities.Doctors;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Application.Doctors.GetDoctors;
using MyClinic.Doctors.Application.Doctors.GetDoctorBy;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Doctors.Application.Doctors.GetDoctorsBySpeciality;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;
using MyClinic.Doctors.Application.Doctors.UpdateDoctor;
using MyClinic.Doctors.Application.Doctors.DeleteDoctor;

namespace MyClinic.Doctors.Application.Doctors.Services;

public interface IDoctorService
{
    Task<Result<PaginationResult<DoctorViewModel>>> GetAllAsync(GetDoctorsQuery query);
    Task<Result<PaginationResult<DoctorViewModel>>> GetAllBySpecialityAsync(GetDoctorsBySpecialityQuery query);
    Task<Result<DoctorDetailsViewModel?>> GetByIdAsync(GetDoctorByIdQuery query);
    Task<Result<DoctorDetailsViewModel?>> GetByAsync(GetDoctorByQuery query);
    Task<Result<Guid>> CreateAsync(CreateDoctorCommand command);
    Task<Result> AddSpecialitiesAsync(Guid id, List<Guid> specialitiesId);
    Task<Result> RemoveSpecialitiesAsync(Guid id, List<Guid> specialitiesId);
    Task<Result> AddSchedulesAsync(Guid id, List<DoctorSchedule> doctorSchedules);
    Task<Result> RemoveSchedulesAsync(Guid id, List<Guid> schedulesId);
    Task<Result> UpdateAsync(Guid id, UpdateDoctorInputModel model);
    Task<Result> DeleteAsync(DeleteDoctorCommand command);
}