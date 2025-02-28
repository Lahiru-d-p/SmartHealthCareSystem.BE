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
		Task<IEnumerable<DoctorAvailability>> GetAllAvailableTimeSlotesByDoctorIdAsync(int id);
		Task<bool> IsTimeSloteOverlappingAsync(int doctorId,DayOfWeek day, TimeSpan startTime, TimeSpan endTime);
		Task<DoctorAvailability> GetDoctorAvailabilityByIdAsync(int id);
		Task<DoctorAvailability> AddDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability);
		Task UpdateDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability);
		Task DeleteDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability);

	}
}
