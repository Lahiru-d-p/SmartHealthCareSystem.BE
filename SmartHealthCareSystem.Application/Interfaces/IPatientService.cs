using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces;

public interface IPatientService
{
	//Get All Patients Details
	Task<List<Patient>> GetAllPatientsAsync();
	//Get All Patients List
	Task<List<UserListModel>> GetAllPatientsListAsync();
	//Get Patient By Id
	Task<Patient> GetPatientByIdAsync(int id);
	//Add Patient
	Task<Patient> AddPatientAsync(PatientInsertModel patient);
	//Update Patient
	Task<Patient> UpdatePatientAsync(PatientUpdateModel patient);
	//Delete Patient
	Task DeletePatientAsync(int id);
}
