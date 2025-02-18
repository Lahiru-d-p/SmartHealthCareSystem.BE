using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;

namespace SmartHealthCareSystem.Application.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentService(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task BookAppointmentAsync(AppointmentInsertModel request)
		{
			var appointment = new Appointment
			{
				AppointmentDate = request.AppointmentDateTime,
				FK_PatientId = request.FK_PatientId,
				FK_DoctorId = request.FK_DoctorId,
				Description = request.Description,
				Status = "Pending"
			};

			await _appointmentRepository.AddAsync(appointment);
		}

		public async Task<List<Appointment>> GetAppointmentsForDoctorAsync(int doctorId)
		{
			return await _appointmentRepository.GetAppointmentsForDoctorAsync(doctorId);
		}

		public async Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId)
		{
			return await _appointmentRepository.GetAppointmentsForPatientAsync(patientId);
		}
	}

}
