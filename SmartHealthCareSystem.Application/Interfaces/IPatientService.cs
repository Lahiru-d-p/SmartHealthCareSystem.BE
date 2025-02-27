using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces;

public interface IPatientService
{
	//Get All Patients Details
	Task<List<PatientViewModel>> GetAllPatientsAsync();
	//Get All Patients List
	Task<List<UserListModel>> GetAllPatientsListAsync();
	//Get Patient By Id
	Task<PatientViewModel> GetPatientByIdAsync(int id);
	//Add Patient
	Task<PatientViewModel> AddPatientAsync(PatientInsertModel patient);
	//Update Patient
	Task<PatientViewModel> UpdatePatientAsync(PatientUpdateModel patient);
	//Delete Patient
	Task DeletePatientAsync(int id);
}
