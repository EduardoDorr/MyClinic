using MyClinic.Common.Results;
using MyClinic.Common.Models.Pagination;
using MyClinic.Patients.Application.Patients.Models;
using MyClinic.Patients.Application.Patients.GetPatient;
using MyClinic.Patients.Application.Patients.GetPatients;
using MyClinic.Patients.Application.Patients.CreatePatient;
using MyClinic.Patients.Application.Patients.UpdatePatient;
using MyClinic.Patients.Application.Patients.DeletePatient;

namespace MyClinic.Patients.Application.Patients.Services;

public interface IPatientService
{
    Task<Result<PaginationResult<PatientViewModel>>> GetAllAsync(GetPatientsQuery query);
    Task<Result<PatientViewModel?>> GetByIdAsync(GetPatientQuery query);
    Task<Result<Guid>> CreateAsync(CreatePatientCommand command);
    Task<Result> UpdateAsync(Guid id, UpdatePatientInputModel model);
    Task<Result> Delete(DeletePatientCommand command);
}