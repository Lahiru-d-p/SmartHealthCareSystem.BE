using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IAppointmentService
    {
		Task BookAppointmentAsync(AppointmentInsertModel request);
		Task<List<Appointment>> GetAppointmentsForDoctorAsync(int doctorId);
		Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId);
	}
}
