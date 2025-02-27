using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Common.Utilities;
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
	public async Task<List<PatientViewModel>> GetAllPatientsAsync()
	{
		try
		{
			var patients = new List<PatientViewModel>();
			var result = await _patientRepository.GetAllPatientsAsync();
			if (result != null)
			{
				patients = result.Select(d => new PatientViewModel
				{
					Id = d.Id,
					FirstName = d.FirstName,
					LastName = d.LastName,
					Address = d.Address,
					DateOfBirth = d.DateOfBirth,
					ContactEMail = d.ContactEMail,
					ContactNumber = d.ContactNumber,
					MedicalHistory = d.MedicalHistory,
					NIC = d.NIC,
					Role = d.Role
				}).ToList();
			}
			return patients;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patients retrieval failed.", ex);
		}
	}

	public async Task<List<UserListModel>> GetAllPatientsListAsync()
	{
		try
		{
			var patients = new List<UserListModel>();
			var result = await _patientRepository.GetAllPatientsAsync();
			if (result != null)
			{
				patients = result.Select(d => new UserListModel { Id = d.Id, Name = $"{d.FirstName} {d.LastName}" }).ToList();
			}
			return patients;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patients List retrieval failed.", ex);
		}
	}

	public async Task<PatientViewModel> GetPatientByIdAsync(int id)
	{
		try
		{
			var patientView = new PatientViewModel();
			var patient = await _patientRepository.GetPatientByIdAsync(id);
			if (patient == null)
			{
				throw new KeyNotFoundException("Patient not found.");
			}
			patientView = new PatientViewModel
			{
				Id = patient.Id,
				FirstName = patient.FirstName,
				LastName = patient.LastName,
				Address = patient.Address,
				DateOfBirth = patient.DateOfBirth,
				ContactEMail = patient.ContactEMail,
				ContactNumber = patient.ContactNumber,
				MedicalHistory = patient.MedicalHistory,
				NIC = patient.NIC,
				Role = patient.Role
			};
			return patientView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patient retrieval failed.", ex);
		}
	}
	public async Task<Patient> AddPatientAsync(PatientInsertModel patientModel) 
	{
		try
		{
			if (await _patientRepository.IsPatientWithEmailAsync(patientModel.ContactEMail))
			{
				throw new InvalidOperationException("A patient with the provided email already exists.");
			}
			var hashedPassword = PasswordHasherHelper.HashPassword(patientModel.Password);
			var patient = new Patient
			{
				FirstName = patientModel.FirstName,
				LastName = patientModel.LastName,
				ContactEMail = patientModel.ContactEMail,
				ContactNumber = patientModel.ContactNumber,
				PasswordHash = hashedPassword,
				NIC = patientModel.NIC,
				Role = "Patient",
				Address = patientModel.Address,
				DateOfBirth = patientModel.DateOfBirth,
				MedicalHistory = patientModel.MedicalHistory,

			};
			var result = await _patientRepository.AddPatientAsync(patient);
			return result;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patient Insert failed.", ex);
		}
	}

	public async Task<Patient> UpdatePatientAsync(PatientUpdateModel patientModel)
	{
		try
		{
			var patient = await _patientRepository.GetPatientByIdAsync(patientModel.Id);
			if (patient == null)
			{
				throw new KeyNotFoundException("Patient not found.");
			}
		
			patient.ContactNumber = patientModel.ContactNumber;
			patient.NIC = patientModel.NIC;
			patient.Address = patientModel.Address;
			patient.DateOfBirth = patientModel.DateOfBirth;
			patient.FirstName = patientModel.FirstName;
			patient.ContactEMail = patientModel.ContactEMail;
			patient.LastName = patientModel.LastName;
			patient.MedicalHistory = patientModel.MedicalHistory;

			await _patientRepository.UpdatePatientAsync(patient);

			return patient;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patient update failed.", ex);
		}
	}
	public async Task DeletePatientAsync(int id)
	{
		try
		{
			var patient = await _patientRepository.GetPatientByIdAsync(id);
			if (patient == null)
			{
				throw new KeyNotFoundException("Patient not found.");
			}

			await _patientRepository.DeletePatientAsync(patient);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Patient deletion failed.", ex);
		}
	}
}
