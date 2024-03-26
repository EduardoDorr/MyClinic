using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.GetPatients;
using MyClinic.Patients.Application.Patients.GetPatientBy;
using MyClinic.Patients.Application.Patients.GetPatientById;
using MyClinic.Patients.Application.Patients.CreatePatient;
using MyClinic.Patients.Application.Patients.UpdatePatient;
using MyClinic.Patients.Application.Patients.DeletePatient;

namespace MyClinic.Patients.Application.Patients.Services;

public interface IPatientService
{
    Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query);
    Task<Result<PatientDetailsViewModel?>> GetByIdAsync(GetPatientByIdQuery query);
    Task<Result<PatientDetailsViewModel?>> GetByAsync(GetPatientByQuery query);
    Task<Result<Guid>> CreateAsync(CreatePatientCommand command);
    Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model);
    Task<Result> DeleteAsync(DeletePatientCommand command);
}