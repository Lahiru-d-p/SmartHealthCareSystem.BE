using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

		private readonly IAppointmentService _appointmentService;

		public AppointmentsController(IAppointmentService appointmentService)
		{
			_appointmentService = appointmentService;
		}

		// Book an appointment
		[HttpPost("book")]
		public async Task<IActionResult> BookAppointment([FromBody] AppointmentInsertModel request)
		{
			await _appointmentService.BookAppointmentAsync(request);
			return Ok("Appointment booked successfully.");
		}

		// Get appointments for a doctor
		[Authorize(Roles = "Doctor")]
		[HttpGet("doctor-appointments/{doctorId}")]
		public async Task<IActionResult> GetAppointmentsForDoctor(int doctorId)
		{
			var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);
			return Ok(appointments);
		}

		// Get appointments for a patient
		[Authorize(Roles = "Patient")]
		[HttpGet("patient-appointments/{patientId}")]
		public async Task<IActionResult> GetAppointmentsForPatient(int patientId)
		{
			var appointments = await _appointmentService.GetAppointmentsForPatientAsync(patientId);
			return Ok(appointments);
		}

	}
}
