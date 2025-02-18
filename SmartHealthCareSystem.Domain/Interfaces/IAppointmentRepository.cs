using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
		Task AddAsync(Appointment appointment);
		Task<List<Appointment>> GetAppointmentsForDoctorAsync(int doctorId);
		Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId);
	}
}
