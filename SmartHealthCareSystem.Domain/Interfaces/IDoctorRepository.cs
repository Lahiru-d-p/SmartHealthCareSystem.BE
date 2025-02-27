using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Domain.Interfaces
{
    public interface IDoctorRepository
    {
		Task<Doctor> GetDoctorByIdAsync(int id);
		Task<bool> IsDoctorWithEmailAsync(string email);
		Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
		Task<Doctor> AddDoctorAsync(Doctor doctor);
		Task UpdateDoctorAsync(Doctor doctor);
		Task DeleteDoctorAsync(Doctor doctor);
	}
}
