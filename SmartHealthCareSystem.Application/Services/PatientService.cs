using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;

namespace SmartHealthCareSystem.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
	public async Task<Patient> GetPatientByIdAsync(int id) => await _patientRepository.GetPatientByIdAsync(id);
	public async Task<bool> IsPatientWithEmailAsync(string email) => await _patientRepository.IsPatientWithEmailAsync(email);
	public async Task<IEnumerable<Patient>> GetAllPatientsAsync() => await _patientRepository.GetAllPatientsAsync();
	public async Task<Patient> AddPatientAsync(PatientInsertModel patientModel,string hashedPassword) {
		var patient = new Patient
		{
			FirstName = patientModel.FirstName,
			LastName = patientModel.LastName,
			ContactEMail = patientModel.ContactEMail,
			ContactNumber = patientModel.ContactNumber,
			PasswordHash = hashedPassword,
			Role = "Patient",
			Address = patientModel.Address,
			DateOfBirth = patientModel.DateOfBirth,
			MedicalHistory = patientModel.MedicalHistory,

		};
		var result = await _patientRepository.AddPatientAsync(patient);
		return patient;
	}
	public async Task UpdatePatientAsync(Patient patient) => await _patientRepository.UpdatePatientAsync(patient);
	public async Task DeletePatientAsync(int id) => await _patientRepository.DeletePatientAsync(id);
}
