using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Doctors.Application.Doctors.Models;
using MyClinic.Doctors.Application.Doctors.GetDoctorBy;
using MyClinic.Doctors.Application.Doctors.GetDoctors;
using MyClinic.Doctors.Application.Doctors.DeleteDoctor;
using MyClinic.Doctors.Application.Doctors.GetDoctorById;
using MyClinic.Doctors.Application.Doctors.CreateDoctor;
using MyClinic.Doctors.Application.Doctors.UpdateDoctor;

namespace MyClinic.Doctors.Application.Doctors.Services;

public interface IDoctorService
{
    //Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query);
    //Task<Result<PatientDetailsViewModel?>> GetByIdAsync(GetPatientByIdQuery query);
    //Task<Result<PatientDetailsViewModel?>> GetByAsync(GetPatientByQuery query);
    Task<Result<Guid>> CreateAsync(CreateDoctorCommand command);
    //Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model);
    //Task<Result> Delete(DeletePatientCommand command);
}