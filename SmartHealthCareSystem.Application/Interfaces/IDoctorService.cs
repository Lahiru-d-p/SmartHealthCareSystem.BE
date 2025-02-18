using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IDoctorService
    {
		Task<Doctor> GetDoctorByIdAsync(int id);
		Task<bool> IsDoctorWithEmailAsync(string email);
		Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
		Task<Doctor> AddDoctorAsync(DoctorInsertModel doctor, string hashedPassword);
		Task UpdateDoctorAsync(Doctor doctor);
		Task DeleteDoctorAsync(int id);
	}
}
