using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces;

public interface IPatientService
{
	Task<Patient> GetPatientByIdAsync(int id);
	Task<bool> IsPatientWithEmailAsync(string email);
	Task<IEnumerable<Patient>> GetAllPatientsAsync();
	Task<Patient> AddPatientAsync(PatientInsertModel patient, string hashedPassword);
	Task UpdatePatientAsync(Patient patient);
	Task DeletePatientAsync(int id);
}
