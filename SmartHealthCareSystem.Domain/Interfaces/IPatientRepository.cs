using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> GetPatientByIdAsync(int id);
	Task<bool> IsPatientWithEmailAsync(string email);
	Task<IEnumerable<Patient>> GetAllPatientsAsync();
	Task<Patient> AddPatientAsync(Patient patient);
	Task UpdatePatientAsync(Patient patient);
	Task DeletePatientAsync(Patient patient);
}
