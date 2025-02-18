using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Infrastructure.Utilities;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
		private readonly IPatientService _patientService;

		public PatientsController(IPatientService patientService)
		{
			_patientService = patientService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPatients() => Ok(await _patientService.GetAllPatientsAsync());

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPatientById(int id) => Ok(await _patientService.GetPatientByIdAsync(id));


		[HttpPost]
		public async Task<IActionResult> AddPatient([FromBody] PatientInsertModel patientModel)
		{
			if (await _patientService.IsPatientWithEmailAsync(patientModel.ContactEMail)){
				return BadRequest("Email already exists.");
			}
			var hashedPassword = PasswordHasherHelper.HashPassword(patientModel.Password);
			var patient = await _patientService.AddPatientAsync(patientModel, hashedPassword);
			return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
		{
			if (id != patient.Id) return BadRequest();
			await _patientService.UpdatePatientAsync(patient);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePatient(int id)
		{
			await _patientService.DeletePatientAsync(id);
			return NoContent();
		}
	}
}
