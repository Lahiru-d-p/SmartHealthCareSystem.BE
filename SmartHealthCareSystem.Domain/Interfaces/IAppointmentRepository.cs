using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
		Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(int? appointmentId, int? doctorId, int? patientId, DateTime? fromDateTime, DateTime? toDateTime, string? status);
		Task<Appointment> GetAppointmentByIdAsync(int id);
		Task<Appointment> AddAppointmentAsync(Appointment appointment);
		Task UpdateAppointmentAsync(Appointment appointment);
		Task<bool> IsPatientAndDoctorAppointmentWithSameDateAsync(int patientId, int doctorId, DateTime date);
	}
}
