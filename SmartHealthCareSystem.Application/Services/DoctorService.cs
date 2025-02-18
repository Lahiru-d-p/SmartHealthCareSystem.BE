using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;

namespace SmartHealthCareSystem.Application.Services
{
    public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _doctorRepository;

		public DoctorService(IDoctorRepository doctorRepository)
		{
			_doctorRepository = doctorRepository;
		}

		public async Task<Doctor> GetDoctorByIdAsync(int id) => await _doctorRepository.GetDoctorByIdAsync(id);
		public async Task<bool> IsDoctorWithEmailAsync(string email) => await _doctorRepository.IsDoctorWithEmailAsync(email); public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() => await _doctorRepository.GetAllDoctorsAsync();
		public async Task<Doctor> AddDoctorAsync(DoctorInsertModel doctorModel, string hashedPassword) {
			var doctor = new Doctor
			{
				FirstName = doctorModel.FirstName,
				LastName = doctorModel.LastName,
				ContactEMail = doctorModel.ContactEMail,
				ContactNumber = doctorModel.ContactNumber,
				PasswordHash = hashedPassword,
				Role = "Doctor",
				Address = doctorModel.Address,
				Specialty = doctorModel.Specialty,
				LicenseNumber = doctorModel.LicenseNumber,
				ClinicAddress = doctorModel.ClinicAddress,
			};
			var result = await _doctorRepository.AddDoctorAsync(doctor);
			return result;
		}
		public async Task UpdateDoctorAsync(Doctor doctor) => await _doctorRepository.UpdateDoctorAsync(doctor);
		public async Task DeleteDoctorAsync(int id) => await _doctorRepository.DeleteDoctorAsync(id);
	}
}
