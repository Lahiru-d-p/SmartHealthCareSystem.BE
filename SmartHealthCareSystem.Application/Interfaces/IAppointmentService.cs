using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IAppointmentService
	{
		Task<List<AppointmentViewModel>> GetAllAppointmentsAsync(int? appointmentId, int? doctorId, int? patientId, DateTime? fromDateTime, DateTime? toDateTime, string? status);
		Task<List<AppointmentListModel>> GetAllAppointmentsListAsync(int? appointmentId, int? doctorId, int? patientId, DateTime? fromDateTime, DateTime? toDateTime, string? status);
		Task<AppointmentViewModel> GetAppointmentByIDAsync(int id);
		Task<AppointmentViewModel> BookAppointmentAsync(AppointmentInsertModel request);
		Task<AppointmentViewModel> UpdateAppointmentAsync(AppointmentUpdateModel request);
		Task<AppointmentViewModel> UpdateAppointmentPrescriptionAsync(AppointmentPrescriptionUpdateModel model);
		Task<AppointmentViewModel> UpdateAppointmentStatusAsync(AppointmentStatusUpdateModel model);
	}
}
