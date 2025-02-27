using Azure.Core;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Common.Utilities;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using System.Numerics;

namespace SmartHealthCareSystem.Application.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentService(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task<List<AppointmentViewModel>> GetAllAppointmentsAsync(int? appointmentId, int? doctorId, int? patientId, DateTime? fromDateTime, DateTime? toDateTime, string? status)
		{
			try
			{
				var appointments = new List<AppointmentViewModel>();
				var result = await _appointmentRepository.GetAllAppointmentsAsync(appointmentId, doctorId, patientId, fromDateTime, toDateTime, status);
				if (result != null)
				{
					appointments = result.Select(d => ConvertToAppointmentViewModel(d)).ToList();
				}
				return appointments;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Appointment retrieval failed.", ex);
			}
		}
		public async Task<List<AppointmentListModel>> GetAllAppointmentsListAsync(int? appointmentId, int? doctorId, int? patientId, DateTime? fromDateTime, DateTime? toDateTime, string? status)
		{
			try
			{
				var appointments = new List<AppointmentListModel>();
				var result = await _appointmentRepository.GetAllAppointmentsAsync(appointmentId,doctorId,patientId,fromDateTime,toDateTime,status);
				if (result != null)
				{
					appointments = result.Select(d => new AppointmentListModel { Id = d.Id, AppointmentNumber = d.AppointmentNumber}).ToList();
				}
				return appointments;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Appointments List retrieval failed.", ex);
			}
		}
		public async Task<AppointmentViewModel> GetAppointmentByIDAsync(int id)
		{
			try
			{
				var appintmentView = new AppointmentViewModel();
				var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
				if (appointment == null)
				{
					throw new KeyNotFoundException("Appointment not found.");
				}
				appintmentView = ConvertToAppointmentViewModel(appointment);
				return appintmentView;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Appointment retrieval failed.", ex);
			}
		}
		public async Task<AppointmentViewModel> BookAppointmentAsync(AppointmentInsertModel request)
		{
			try
			{
				var appintmentView = new AppointmentViewModel();
				if (!request.ReAppointment && await _appointmentRepository.IsPatientAndDoctorAppointmentWithSameDateAsync(request.FK_PatientId, request.FK_DoctorId, request.AppointmentDateTime))
				{
					throw new InvalidOperationException("Same patient with same doctor having an appointment on same date");
				}
				if (!Enum.TryParse<AppointmentStatus>("SH", true,out var appointmentStatus))
				{
					throw new InvalidOperationException("Invalid appointment status");
				}
				var appointment = new Appointment
				{
					AppointmentDateTime = request.AppointmentDateTime,
					FK_PatientId = request.FK_PatientId,
					FK_DoctorId = request.FK_DoctorId,
					Description = request.Description,
					Status = (int)appointmentStatus
				};
				appointment = await _appointmentRepository.AddAppointmentAsync(appointment);

				appintmentView = ConvertToAppointmentViewModel(appointment);
				return appintmentView;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Appointment Insert failed.", ex);
			}
		}
		public async Task<AppointmentViewModel> UpdateAppointmentAsync(AppointmentUpdateModel updateModel)
		{
			try
			{
				var appintmentView = new AppointmentViewModel();
				var appointment = await _appointmentRepository.GetAppointmentByIdAsync(updateModel.Id);
				if (appointment == null)
				{
					throw new KeyNotFoundException("Appointment not found.");
				}

				appointment.AppointmentDateTime = updateModel.AppointmentDateTime;
				appointment.FK_PatientId = updateModel.FK_PatientId;
				appointment.FK_DoctorId = updateModel.FK_DoctorId;
				appointment.Description = updateModel.Description;

				await _appointmentRepository.UpdateAppointmentAsync(appointment);


				appintmentView = ConvertToAppointmentViewModel(appointment);
				return appintmentView;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Patient update failed.", ex);
			}
		}
		public async Task<AppointmentViewModel> UpdateAppointmentPrescriptionAsync(AppointmentPrescriptionUpdateModel updateModel)
		{
			try
			{
				var appintmentView = new AppointmentViewModel();
				var appointment = await _appointmentRepository.GetAppointmentByIdAsync(updateModel.Id);
				if (appointment == null)
				{
					throw new KeyNotFoundException("Appointment not found.");
				}

				appointment.Prescription = updateModel.Prescription;

				await _appointmentRepository.UpdateAppointmentAsync(appointment);


				appintmentView = ConvertToAppointmentViewModel(appointment);
				return appintmentView;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Patient update failed.", ex);
			}
		}
		public async Task<AppointmentViewModel> UpdateAppointmentStatusAsync(AppointmentStatusUpdateModel updateModel)
		{
			try
			{
				var appintmentView = new AppointmentViewModel();
				var appointment = await _appointmentRepository.GetAppointmentByIdAsync(updateModel.Id);
				if (appointment == null)
				{
					throw new KeyNotFoundException("Appointment not found.");
				}
				if (!Enum.TryParse<AppointmentStatus>(updateModel.Status, true, out var appointmentStatus))
				{
					throw new InvalidOperationException("Invalid appointment status");
				}
				appointment.Status = (int)appointmentStatus;

				await _appointmentRepository.UpdateAppointmentAsync(appointment);

				appintmentView = ConvertToAppointmentViewModel(appointment);
				return appintmentView;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Patient update failed.", ex);
			}
		}
		private AppointmentViewModel ConvertToAppointmentViewModel(Appointment appointment)
		{
			return new AppointmentViewModel
			{
				Id = appointment.Id,
				AppointmentDateTime = appointment.AppointmentDateTime,
				AppointmentNumber = appointment.AppointmentNumber,
				Description = appointment.Description,
				FK_DoctorId = appointment.FK_DoctorId,
				FK_PatientId = appointment.FK_PatientId,
				Prescription = appointment.Prescription,
				Status = appointment.Status,
				StatusDescription = ((AppointmentStatus)appointment.Status).GetDescription(),
				Doctor = new DoctorViewModel
				{
					Id = appointment.Doctor.Id,
					FirstName = appointment.Doctor.FirstName,
					LastName = appointment.Doctor.LastName,
					Address = appointment.Doctor.Address,
					ClinicAddress = appointment.Doctor.ClinicAddress,
					ContactEMail = appointment.Doctor.ContactEMail,
					ContactNumber = appointment.Doctor.ContactNumber,
					LicenseNumber = appointment.Doctor.LicenseNumber,
					NIC = appointment.Doctor.NIC,
					Role = appointment.Doctor.Role,
					Specialty = appointment.Doctor.Specialty
				},
				Patient = new PatientViewModel
				{
					Id = appointment.Patient.Id,
					FirstName = appointment.Patient.FirstName,
					LastName = appointment.Patient.LastName,
					Address = appointment.Patient.Address,
					DateOfBirth = appointment.Patient.DateOfBirth,
					ContactEMail = appointment.Patient.ContactEMail,
					ContactNumber = appointment.Patient.ContactNumber,
					MedicalHistory = appointment.Patient.MedicalHistory,
					NIC = appointment.Patient.NIC,
					Role = appointment.Patient.Role
				}
			};
		}

	}

}
